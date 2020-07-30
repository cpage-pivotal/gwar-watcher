using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.Message
{
    public class AuthenticatedServerSideMessageValidator : MessageValidatorBase, IMessageValidator, IValidator
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                //{ ( validation: m => m.EventId > 0 , message: "Event Id is not set" ) }
            }));
            return toReturn;
        }
    }
}
