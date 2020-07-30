using OTF.GwarWatcher.Validators.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{

    public abstract class ValidatorBase: IValidator
    {
    }

    public abstract class ValidatorBase<MT>: ValidatorBase, IValidator<Models.MessageModel>
        where MT : IMessageValidator, new()
    {
        protected MT MessageValidator => new MT();

        public virtual ValidatorResult Validate(Models.MessageModel message)
        {
            ValidatorResult toReturn = this.MessageValidator.Validate(message);

            List<(Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message)> validators = new List<(Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message)>();
            if (this is IMessageValueRequired)
            {
                validators.Add(Rules.RequiredStringPropertyRule(m => m.Value, "Value"));
            }

            if (this is IMessageLabelRequired)
            {
                validators.Add(Rules.RequiredStringPropertyRule(m => m.Label, "Label"));
            }

            if(validators.Any())
            {
                toReturn.Concat(message.RunValidation(validators));
            }

            return toReturn;
        }
    }
}
