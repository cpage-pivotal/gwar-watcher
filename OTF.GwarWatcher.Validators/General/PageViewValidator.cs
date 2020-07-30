using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.General
{
    [EventValidator(eventId: 0)]
    public class PageViewValidator : WithPayloadValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator, IMessageValueRequired
    {
        protected override IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators => new List<IPayloadPropertyValidator>()
        {
            new PageUrlValidator(),
            new PageTitleValidator()
        };

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.RunValidation(new List<(Func<MessageModel, bool> validation, Func<MessageModel, string> message)>()
            {
                { Rules.CategoryValueRule(new List<string>() { "General", "MA Home", "PDP", "Cart", "SERP", "Order History", "Category", "Support", "Manufacturer", "Deals" }) },
                { Rules.ActionValueRule("Page View") },
                { Rules.MessageFieldShouldEqualPayloadProperty(m => m.Value, "Value", "PageUrl") }
            }));
            return toReturn;
        }

    }
}
