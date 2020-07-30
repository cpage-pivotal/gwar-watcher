using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Pdp
{
    [EventValidator(eventId: 7)]
    public class EmailProductValidator: WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new ProductIdValidator(),
            new ProductPriceValidator()
        };

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("PDP") },
                { Rules.ActionValueRule("Email Product") },
                { ( validation: m => string.Equals(m.Value, m.PayloadAsJObject?.GetPropertyAsString("ProductId") ?? string.Empty, StringComparison.InvariantCultureIgnoreCase), message: m => "Value is not set to Product ID" ) },
            }));
            return toReturn;
        }
    }
}

