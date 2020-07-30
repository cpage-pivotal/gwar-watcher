using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Ad
{
    public class ImageUrlValidator : UrlPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public ImageUrlValidator(UriKind uriKind = UriKind.Absolute) 
        {
            this.UriKindType = uriKind;
        }
        public override string PropertyName => "ImageUrl";
        public override UriKind UriKindType { get; } = UriKind.Absolute;
    }
}
