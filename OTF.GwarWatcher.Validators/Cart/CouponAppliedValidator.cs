using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Cart
{
    [EventValidator(eventId: 24)]
    public class CouponAppliedValidator : WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new CouponCodePropertyValidator(),
        };

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("Cart") },
                { Rules.ActionValueRule("Coupon Applied") },
                { Rules.ValuePayloadPropertyRule("CouponCode") }
            }));
            return toReturn;
        }

    }
}
