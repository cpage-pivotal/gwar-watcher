using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Pdp
{
    [EventValidator(eventId: 12)]
    public class ViewPdfValidator : ValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageValueRequired, IMessageLabelRequired
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule("PDP") },
                { Rules.ActionValueRule("Product PDF") },
                { ( validation: m => string.Equals(m.Value, "View", StringComparison.InvariantCultureIgnoreCase), message: m => "Value is not set to \"View\"" ) },
            }));
            return toReturn;
        }
    }
}
