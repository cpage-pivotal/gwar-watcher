using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty
{
    public interface IPayloadPropertyValidator: IValidator<JObject>
    {
        string PropertyName { get; }
    }
}
