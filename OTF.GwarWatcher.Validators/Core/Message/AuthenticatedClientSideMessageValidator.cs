using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.Message
{
    public class AuthenticatedClientSideMessageValidator : AuthenticatedMessageValidatorBase, IMessageValidator, IValidator<MessageModel>
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {

            }));
            return toReturn;
        }
    }
}
