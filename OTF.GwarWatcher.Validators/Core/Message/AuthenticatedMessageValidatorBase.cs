using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.Message
{
    public abstract class AuthenticatedMessageValidatorBase : MessageValidatorBase, IValidator<MessageModel>
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.RequiredStringPropertyRule(m => m.OpCo, "OPCO") },
                { Rules.RequiredStringPropertyRule(m => m.Role, "Role") },
                { Rules.RequiredStringPropertyRule(m => m.AccountNumber, "Account Number") },
                { Rules.RequiredStringPropertyRule(m => m.UserEmailAddress, "User Email Address") },
                { Rules.RequiredInt32PropertyRule(m => m.UserId, "User ID") },
                { Rules.RequiredInt32PropertyRule(m => m.OboUserId, "OBO User ID", -1) } // 0 is a valid value

            }));
            return toReturn;
        }
    }
}
