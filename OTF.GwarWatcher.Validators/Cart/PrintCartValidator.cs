using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Cart
{
    [EventValidator(eventId: 25)]
    public class PrintCartValidator : ProductCollectionPayloadValidatorBase, IValidator
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("Cart") },
                { Rules.ActionValueRule("Print") },
            }));
            return toReturn;
        }
    }
}
