using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    public abstract class SingleProductWithPriceValidator : SingleProductValidator, IValidator, IMessageLabelRequired, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => base.PayloadPropertyValidators.Concat(new List<IPayloadPropertyValidator>() { new ProductPriceValidator() });
    }
}
