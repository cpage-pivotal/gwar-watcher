using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class EventValidatorAttribute : Attribute
    {
        public EventValidatorAttribute(int eventId)
        {
            this.EventId = eventId;
        }
        
        public int EventId { get; }
    }
}
