using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Product
{
    public class OrderProductsValidator : ProductsValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadItemPropertyValidators => base.PayloadItemPropertyValidators.Concat(new List<IPayloadPropertyValidator>()
        {
            new ProductNumberValidator(),
            new ProductUnitPriceValidator(),
        });
    }
}
