using Newtonsoft.Json.Linq;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Checkout
{
    public class QuotesPropertyValidator : CollectionPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "Quotes";
        protected override IEnumerable<IPayloadPropertyValidator> PayloadItemPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            //new ProductIdValidator(),
            //new ProductNumberValidator(),
            //new ProductQuantityValidator(),
            //new ProductUnitPriceValidator(),
            //new ProductPriceValidator()
        };
    }
}
