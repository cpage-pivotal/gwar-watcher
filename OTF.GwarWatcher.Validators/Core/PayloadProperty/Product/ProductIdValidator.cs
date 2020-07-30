using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty.Product
{
    public class ProductIdValidator : Int32PropertyValidator, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override string PropertyName => "ProductId";
    }
}
