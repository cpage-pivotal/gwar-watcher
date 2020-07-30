using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty
{
    public abstract class PayloadPropertyValidatorBase : IPayloadPropertyValidator, IValidator<JObject>
    {
        public abstract string PropertyName { get; }
        public abstract ValidatorResult Validate(JObject payload);
    }
}
