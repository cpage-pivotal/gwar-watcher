using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Ad
{
    public class TargetUrlValidator : UrlPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "TargetUrl";
    }
}
