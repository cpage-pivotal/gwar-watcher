using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Checkout;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Checkout
{
    [EventValidator(eventId: 28)]
    public class OrderPlacedValidator : WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageLabelRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new OrdersPropertyValidator(),
            new QuotesPropertyValidator(),
            new PaymentMethodPropertyValidator(),
            new ShippingAmountPropertyValidator(),
            new MasterOrderNumberPropertyValidator(),
            new MasterOrderIdPropertyValidator()
        };
    }
}
