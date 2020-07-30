using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Product
{
    public class ProductsValidator : CollectionPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "Products";
        protected override IEnumerable<IPayloadPropertyValidator> PayloadItemPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new ProductIdValidator(),
            new ProductQuantityValidator(),
            new ProductPriceValidator()
        };

    }
}
