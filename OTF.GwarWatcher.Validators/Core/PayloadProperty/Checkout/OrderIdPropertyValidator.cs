using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Checkout
{
    public class OrderIdPropertyValidator : Int32PropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "OrderId";
    }
}
