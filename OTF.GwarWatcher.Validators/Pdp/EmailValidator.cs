using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Pdp
{
    [EventValidator(eventId: 9)]
    public class EmailValidator : ValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageValueRequired
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("PDP") },
                { Rules.ActionValueRule("Need Assistance") },
                { ( validation: m => string.Equals(m.Value, "email", StringComparison.InvariantCultureIgnoreCase), message: m => "Value is not set to \"email\"" ) },
            }));
            return toReturn;
        }
    }
}
