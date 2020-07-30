using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.Message
{
    public abstract class MessageValidatorBase : IMessageValidator, IValidator<MessageModel>
    {
        public virtual ValidatorResult Validate(MessageModel message)
        {
            return message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.RequiredInt32PropertyRule(m => m.EventId, "Event ID", -1) },
                { Rules.RequiredStringPropertyRule(m => m.Category, "Category") },
                { Rules.RequiredStringPropertyRule(m => m.Action, "Action") },
                { Rules.RequiredStringPropertyRule(m => m.IpAddress, "IP Address") },
                { Rules.RequiredStringPropertyRule(m => m.SourcePageTitle, "Source Page Title") },
                { Rules.RequiredStringPropertyRule(m => m.SourcePageUrl, "Source Page URL") },
                { Rules.RequiredStringPropertyRule(m => m.Timestamp, "Timestamp") },
                { Rules.RequiredStringPropertyRule(m => m.ReporterKey, "Reporter Key") },
                { Rules.RequiredInt32PropertyRule(m => m.SiteId, "Site ID") },
                { Rules.RequiredStringPropertyRule(m => m.Component, "Analytics Component") },
                { Rules.RequiredStringPropertyRule(m => m.BrowserName, "Browser Name") },
                { Rules.RequiredStringPropertyRule(m => m.BrowserVersion, "Browser Version") },
                { Rules.RequiredStringPropertyRule(m => m.DeviceCategory, "Device Category") },
                { Rules.RequiredStringPropertyRule(m => m.BrowserSize, "Browser Size") },
                { Rules.RequiredStringPropertyRule(m => m.BrowserViewport, "Browser Viewport") },
                { Rules.RequiredStringPropertyRule(m => m.OperatingSystem, "Operating System") },
                { Rules.RequiredStringPropertyRule(m => m.OperatingSystemVersion, "Operating System Version") },
                { Rules.RequiredPropertyRule(m => m.Server, "Server metadata object") },
                { Rules.RequiredStringPropertyRule(m => m.Server?.Timestamp, "Server timestamp") }
            });
        }
    }
}
