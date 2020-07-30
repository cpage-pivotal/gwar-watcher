using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Ad;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Pdp
{
    [EventValidator(eventId: 15)]
    public class ProductImageValidator : WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new ImageUrlValidator(UriKind.Relative)
        };

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("PDP") },
                { Rules.ActionValueRule("Product Image") },
                { Rules.ValuePayloadPropertyRule("ImageUrl") }
            }));
            return toReturn;
        }
    }
}