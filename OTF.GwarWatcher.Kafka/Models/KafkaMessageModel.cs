using System;
using System.Collections.Generic;
using System.Text;
using OTF.GwarWatcher.Models;

namespace OTF.GwarWatcher.Kafka.Models
{
    public class KafkaMessageModel
    {
        public string Topic { get; set; } = string.Empty;
        public MessageModel Value { get; set; } = new MessageModel();
        public string Raw { get; set; } = string.Empty;
    }
}
