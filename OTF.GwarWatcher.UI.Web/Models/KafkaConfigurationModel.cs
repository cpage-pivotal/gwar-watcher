using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTF.GwarWatcher.UI.Web.Models
{
    public class KafkaConfigurationModel
    {
        public string GroupId { get; set; } = string.Empty;
        public string BootstrapServers { get; set; } = string.Empty;
        public string[] Topics { get; set; } = new string[0];
        public int MaxMessageCount { get; set; } = -1;
        public int TimeoutSeconds { get; set; } = 10;
    }
}
