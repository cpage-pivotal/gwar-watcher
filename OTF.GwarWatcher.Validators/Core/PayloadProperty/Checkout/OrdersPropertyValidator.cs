using Newtonsoft.Json.Linq;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Checkout
{
    public class OrdersPropertyValidator : CollectionPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "Orders";
        protected override IEnumerable<IPayloadPropertyValidator> PayloadItemPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new OrderIdPropertyValidator(),
            new OrderProductsValidator()
        };
    }
}
