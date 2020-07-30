using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.Message
{
    public abstract class UnauthenticatedMessageValidatorBase : MessageValidatorBase, IValidator<MessageModel>
    {
    }
}
