using Core = OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Kafka.Models;
using OTF.GwarWatcher.Validators.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using OTF.GwarWatcher.Common.Utilities;

namespace OTF.GwarWatcher.UI.Web.Models
{
    public class MessageModel
    {
        public MessageModel() { }
        public MessageModel(EventModel eventData)
        {
            this.Event = eventData;
            this.FoundInKafka = false;
            this.FoundInElastic = false;
        }
        public EventModel Event { get; set; } = new EventModel();
        public KafkaMessageModel KafkaMessage { get; set; } = new KafkaMessageModel();
        public bool FoundInKafka { get; set; } = false;
        public ValidatorResult Result { get; set; } = new ValidatorResult();
        public bool FoundInElastic { get; set; } = false;
        public Core.MessageModel ElasticMessage { get; set; } = new Core.MessageModel();
        public string KibanaUrl { get; set; } = string.Empty;
        public DateTime MaxTimestamp => MathUtility.Max((this.KafkaMessage?.Value?.TimestampDateTime).GetValueOrDefault(DateTime.MinValue), (this.ElasticMessage?.TimestampDateTime).GetValueOrDefault(DateTime.MinValue));

        public static MessageModel FromKafkaMessage(KafkaMessageModel kafkaMessage, ValidatorResult result) => new MessageModel()
        {
            KafkaMessage = kafkaMessage,
            FoundInKafka = kafkaMessage != null,
            Result = result,
            Event = new EventModel(kafkaMessage)
        };

        public static MessageModel FromElasticMessage(Core.MessageModel elasticMessage, ValidatorResult result, ElasticConfigurationModel config) => new MessageModel()
        {
            ElasticMessage = elasticMessage,
            FoundInElastic = elasticMessage != null,
            Result = result,
            Event = new EventModel(elasticMessage),
            KibanaUrl = MessageModel.GetKibanaUrl(elasticMessage, config)
        };

        public static string GetKibanaUrl(Core.MessageModel elasticMessage, ElasticConfigurationModel config) =>
            elasticMessage != null
            ? $"{config.KibanaRoot}/app/kibana#/discover?_g=(time:(from:'{elasticMessage.TimestampDateTime.GetValueOrDefault(DateTime.MinValue).AddMinutes(-30):o}',to:now))&_a=(columns:!(_source),filters:!((query:(match:(actionId:(query:'{elasticMessage?.ActionId}',type:phrase))))))"
            : string.Empty;
    }
}
