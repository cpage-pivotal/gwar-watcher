using Newtonsoft.Json.Linq;
using OTF.GwarWatcher.Common.Extensions;
using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Cart
{
    [EventValidator(eventId: 21)]
    public class SaveCartValidator : ProductCollectionPayloadValidatorBase, IValidator
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("Cart") },
                { Rules.ActionValueRule("Save") },
                { (validation: m => !string.IsNullOrEmpty(m.Value), message: m => "Value was empty instead of set to cart name") }
            }));
            return toReturn;
        }
    }
}
