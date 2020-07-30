using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTF.GwarWatcher.UI.Web.Models
{
    public class ElasticConfigurationModel
    {
        public string[] Nodes { get; set; } = new string[0];
        public int MaxValuesPerQuery { get; set; } = 1000;
        public string KibanaRoot { get; set; } = string.Empty;
        public string EnvironmentTldCsv { get; set; } = string.Empty;
    }
}
