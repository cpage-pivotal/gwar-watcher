using OTF.GwarWatcher.Kafka.Models;
using Core = OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTF.GwarWatcher.UI.Web.Models
{
    public class EventModel
    {
        public EventModel() { }
        public EventModel(Core.MessageModel message)
        {
            this.Id = (message?.EventId).GetValueOrDefault(-1);
            this.ActionId = (message?.ActionId).GetValueOrDefault(Guid.Empty);
            this.Category = message?.Category ?? string.Empty;
            this.Action = message?.Action ?? string.Empty;
            this.Timestamp = message?.TimestampDateTime;
        }
        public EventModel(KafkaMessageModel kafkaMessage) : this(kafkaMessage?.Value) { }
        public int Id { get; set; } = -1;
        public Guid ActionId { get; set; } = Guid.Empty;
        public string Category { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DateTime? Timestamp { get; set; } = null;
    }
}
