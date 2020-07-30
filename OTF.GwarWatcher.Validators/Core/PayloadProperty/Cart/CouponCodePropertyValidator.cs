using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Cart
{
    public class CouponCodePropertyValidator : StringPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "CouponCode";
    }
}
