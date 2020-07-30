using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty
{
    public abstract class UrlPropertyValidator : StringPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public virtual UriKind UriKindType => UriKind.RelativeOrAbsolute;
        public override ValidatorResult Validate(JObject payload)
        {
            ValidatorResult toReturn = base.Validate(payload);
            toReturn.Concat(payload.RunValidation(new List<(Func<JObject, bool> validation, Func<JObject, string> message)>()
            {
                (validation: jo => Uri.IsWellFormedUriString((jo.GetPropertyAsString(this.PropertyName)), this.UriKindType), message: jo => $"{this.PropertyName} is not a well form URI string" )
            }));
            return toReturn;
        }
    }
}
