using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Page
{
    public class PageUrlValidator : UrlPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "PageUrl";
        public override UriKind UriKindType => UriKind.Absolute;
    }
}
