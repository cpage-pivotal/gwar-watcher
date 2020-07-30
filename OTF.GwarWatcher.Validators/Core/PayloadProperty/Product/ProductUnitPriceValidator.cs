using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Product
{
    public class ProductUnitPriceValidator : FloatPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "UnitPrice";
    }
}
