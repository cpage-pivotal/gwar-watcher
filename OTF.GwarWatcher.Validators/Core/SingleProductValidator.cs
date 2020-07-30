using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    public abstract class SingleProductValidator: WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageLabelRequired, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new ProductIdValidator(),
            new ProductQuantityValidator()
        };

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.ValuePayloadPropertyRule("ProductId") },
                { Rules.LabelPayloadPropertyRule("Quantity") }
            }));
            return toReturn;
        }
    }
}
