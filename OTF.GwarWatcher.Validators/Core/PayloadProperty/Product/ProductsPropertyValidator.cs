using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Product
{
    public class ProductsPropertyValidator : CollectionPropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "Products";

        protected override IEnumerable<IPayloadPropertyValidator> PayloadItemPropertyValidators => throw new NotImplementedException();
    }
}
