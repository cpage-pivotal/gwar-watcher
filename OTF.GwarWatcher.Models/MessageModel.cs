using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Models
{
    public class MessageModel
    {
        public class ServerMetadataModel
        {
            public string Timestamp { get; set; } = string.Empty;
            public DateTime? TimestampDateTime => MessageModel.TimestampToDateTime(this.Timestamp);
        }
        public Guid ActionId { get; set; } = Guid.Empty;
        public int EventId { get; set; } = 0;
        public string IpAddress { get; set; } = string.Empty;
        public string SourcePageTitle { get; set; } = string.Empty;
        public string SourcePageUrl { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public DateTime? TimestampDateTime => MessageModel.TimestampToDateTime(this.Timestamp);
        public string Category { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string ReporterKey { get; set; } = string.Empty;
        public int SiteId { get; set; } = 0;
        public string Component { get; set; } = string.Empty;
        public string OpCo { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string UserEmailAddress { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
        public int OboUserId { get; set; } = 0;
        public string BrowserName { get; set; } = string.Empty;
        public string BrowserVersion { get; set; } = string.Empty;
        public string DeviceCategory { get; set; } = string.Empty;
        public string BrowserSize { get; set; } = string.Empty;
        public string BrowserViewport { get; set; } = string.Empty;
        public string OperatingSystem { get; set; } = string.Empty;
        public string OperatingSystemVersion { get; set; } = string.Empty;
        public ServerMetadataModel Server { get; set; } = new ServerMetadataModel();
        public dynamic Payload { get; set; }
        public JObject PayloadAsJObject => this.Payload is JObject
            ? this.Payload as JObject
            : (this.Payload is Dictionary<string, object> ? JObject.FromObject(this.Payload) : null);
        public JArray PayloadAsJArray => this.Payload is JArray
            ? this.Payload as JArray
            : (this.Payload is List<object> ? JArray.FromObject(this.Payload) : null);

        private static DateTime? TimestampToDateTime(string timestamp) => !string.IsNullOrEmpty(timestamp) ? DateTime.Parse(timestamp, null, System.Globalization.DateTimeStyles.RoundtripKind) : new DateTime?();
    }
}
