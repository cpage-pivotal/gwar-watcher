using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Ad;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    public abstract class SingleAdValidator : WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageLabelRequired, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new AdIdValidator(),
            //new ContentSpaceIdValidator(),
            new CurrentUrlValidator(),
            new PageValidator(),
            new ImageTextValidator(),
            new ImageUrlValidator(),
            new TargetUrlValidator(),
            //new ModelIdValidator(),
            //new ModuleIdValidator()
        };

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { ( validation: m => string.Equals(m.Value, m.PayloadAsJObject?.GetPropertyAsString("ImageText") ?? string.Empty, StringComparison.InvariantCultureIgnoreCase), message: m => "Value is not set to Image Text" ) },
                { ( validation: m => string.Equals(m.Label, m.PayloadAsJObject?.GetPropertyAsString("ContentSpaceId") ?? string.Empty, StringComparison.InvariantCultureIgnoreCase), message: m => "Label is not set to Content Space ID" ) }
            }));
            return toReturn;
        }

    }
}
