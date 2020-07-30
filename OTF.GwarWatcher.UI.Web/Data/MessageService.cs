using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core = OTF.GwarWatcher.Models;
using Kafka = OTF.GwarWatcher.Kafka;
using Elastic = OTF.GwarWatcher.Elastic;
using OTF.GwarWatcher.UI.Web.Models;
using OTF.GwarWatcher.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Confluent.Kafka;
using OTF.GwarWatcher.Validators;
using Microsoft.Extensions.Options;

namespace OTF.GwarWatcher.UI.Web.Data
{
    public class MessageService
    {
        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }
        public MessageService(IWebHostEnvironment environment, IConfiguration config)
        {
            this.Environment = environment;
            this.Configuration = config;
        }

        public async Task<IEnumerable<MessageModel>> GetMessages(string environment)
        {
            List<MessageModel> toReturn = new List<MessageModel>();
            // Get Kafka messages
            List<Kafka.Models.KafkaMessageModel> kafkaMessages = new List<Kafka.Models.KafkaMessageModel>();
            KafkaConfigurationModel kConfig = this.GetKafkaConfiguration(environment);
            Kafka.MessageProvider kmProvider = new Kafka.MessageProvider()
            {
                GroupId = kConfig.GroupId,
                BootstrapServers = kConfig.BootstrapServers,
                TimeoutSeconds = kConfig.TimeoutSeconds
            };
            await kConfig.Topics.ForEachAsync(async t => kafkaMessages.AddRange(await kmProvider.GetMessages(t, kConfig.MaxMessageCount)));

            // Validate messages
            ValidatorProvider vProvider = new ValidatorProvider();
            toReturn.AddRange(kafkaMessages.Select(k => MessageModel.FromKafkaMessage(k, vProvider.Validate(k.Value))));

            // Get elastic messages
            ElasticConfigurationModel eConfig = this.GetElasticConfiguration(environment);
            Elastic.MessageProvider emProvider = new Elastic.MessageProvider()
            {
                Nodes = eConfig.Nodes,
                MaxValuesPerQuery = eConfig.MaxValuesPerQuery
            };

            // Check action ID for those found in kafka
            IEnumerable<Core.MessageModel> actionIdMessages = await emProvider.GetMessages(m => m.ActionId, toReturn.Where(m => m.FoundInKafka).Select(m => m.Event.ActionId).ToList(), eConfig.EnvironmentTldCsv);
            actionIdMessages
                .ForEach(em =>
                {
                    MessageModel m = toReturn.FirstOrDefault(m => m.FoundInKafka && m.Event.ActionId == em.ActionId);
                    if(m != null)
                    {
                        m.FoundInElastic = true;
                        m.KibanaUrl = MessageModel.GetKibanaUrl(em, eConfig);
                    }
                });

            // Add in messages missing in Kafka, and get events from elastic
            EventModel[] allEvents = this.Configuration.GetSection("Events").Get<EventModel[]>();
            int[] currentEventIds = toReturn.Select(m => m.Event.Id).Distinct().ToArray();
            await allEvents.Where(e => !currentEventIds.Contains(e.Id)).ForEachAsync(async e =>
            {
                IEnumerable<Core.MessageModel> elasticMessages = await emProvider.GetMessages(m => m.EventId, new List<int>() { e.Id }, eConfig.EnvironmentTldCsv, this.Configuration.GetSection("NumberOfElasticEventsForMissingFromKafka").Get<int>());
                if (!elasticMessages.Any())
                {
                    toReturn.Add(new MessageModel(e));
                }
                else
                {
                    toReturn.AddRange(elasticMessages.Select(m => MessageModel.FromElasticMessage(m, vProvider.Validate(m), eConfig)));
                }
            });

            return toReturn;
        }

        private KafkaConfigurationModel GetKafkaConfiguration(string environment) => this.GetConfiguration<KafkaConfigurationModel>("kafkaConfig", environment);

        private ElasticConfigurationModel GetElasticConfiguration(string environment) => this.GetConfiguration<ElasticConfigurationModel>("elasticConfig", environment);

        private T GetConfiguration<T>(string configFilename, string environment)
        {
            return new ConfigurationBuilder()
                .SetBasePath(this.Environment.ContentRootPath)
                .AddJsonFile($"{configFilename}.json")
                .AddJsonFile($"{configFilename}.{environment}.json", optional: true)
                .Build()
                .Get<T>();
        }
    }
}
