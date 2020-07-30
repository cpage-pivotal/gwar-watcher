using Confluent.Kafka;
using Newtonsoft.Json;
using OTF.GwarWatcher.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTF.GwarWatcher.Kafka
{
    public class MessageProvider
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 10;
        public async Task<IEnumerable<Models.KafkaMessageModel>> GetMessages(string topic, long count = -1)
        {
            List<Models.KafkaMessageModel> toReturn = new List<Models.KafkaMessageModel>();
            if(!string.IsNullOrEmpty(this.BootstrapServers) && !string.IsNullOrEmpty(this.GroupId))
            {
                ConsumerConfig config = new ConsumerConfig()
                {
                    BootstrapServers = this.BootstrapServers,
                    GroupId = this.GroupId,
                    AutoOffsetReset = AutoOffsetReset.Latest
                };
                await Task.Run(() =>
                {
                    using (IConsumer<string, string> consumer = new ConsumerBuilder<string, string>(config).Build())
                    {
                        ConsumeResult<string, string> result = null;
                        try
                        {
                            consumer.Subscribe(topic);
                            while (!consumer.Assignment.Any()) { }

                            TopicPartition tp = consumer.Assignment.FirstOrDefault();
                            WatermarkOffsets wo = consumer.QueryWatermarkOffsets(tp, TimeSpan.FromSeconds(this.TimeoutSeconds));

                            long numMessages = wo.High - wo.Low;
                            if (count > 0 && count < numMessages)
                            {
                                numMessages = count;
                            }

                            consumer.Seek(new TopicPartitionOffset(tp, wo.High - numMessages));
                            do
                            {
                                result = consumer.Consume(TimeSpan.FromSeconds(this.TimeoutSeconds));
                                if (result != null)
                                {
                                    try
                                    {
                                        toReturn.Add(new Models.KafkaMessageModel()
                                        {
                                            Topic = result.Topic,
                                            Value = JsonConvert.DeserializeObject<MessageModel>(result.Message.Value),
                                            Raw = result.Message.Value
                                        });
                                    }
                                    catch(JsonSerializationException) { } /* We may add events in the future, and don't want to stop collecting current events if we haven't accounted for the structure */
                                }
                            } while (result != null && result.TopicPartitionOffset.Offset.Value <= wo.High - 1);

                        }
                        catch (Exception){ }

                        consumer.Unsubscribe();
                        consumer.Close();
                    }
                });
            }
            return toReturn;
        }
    }
}
