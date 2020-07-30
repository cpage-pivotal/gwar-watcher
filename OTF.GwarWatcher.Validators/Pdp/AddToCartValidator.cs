using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OTF.GwarWatcher.Validators.Pdp
{
    [EventValidator(eventId: 1)]
    public class AddToCartValidator : SingleProductWithPriceValidator, IValidator, IMessageLabelRequired, IMessageValueRequired
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("PDP") },
                { Rules.ActionValueRule("Add to Cart") }
            }));
            return toReturn;
        }
    }
}
