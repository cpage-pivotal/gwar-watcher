using Newtonsoft.Json.Linq;
using OTF.GwarWatcher.Common.Extensions;
using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using OTF.GwarWatcher.Validators.Core.PayloadProperty.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    public abstract class ProductCollectionPayloadValidatorBase : ValidatorBase<AuthenticatedClientSideMessageValidator>, IValidator<MessageModel>
    {
        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);

            if (message.PayloadAsJArray != null)
            {
                if (message.PayloadAsJArray.Any())
                {
                    int itemIdx = 0;
                    IEnumerable<IPayloadPropertyValidator> payloadPropertyValidators = new List<IPayloadPropertyValidator>()
                    {
                        new ProductIdValidator(),
                        new ProductQuantityValidator()
                    };
                    message.PayloadAsJArray.ForEach(t =>
                    {
                        IEnumerable<ValidatorResult> results = payloadPropertyValidators.Select(v => v.Validate(t as JObject));
                        toReturn.Concat(results.Where(r => r.Messages.Any()).Select(r => new ValidatorResult()
                        {
                            IsValid = r.IsValid,
                            Messages = r.Messages.Select(m => $"[Item {itemIdx}] {m}")
                        }));
                        itemIdx++;
                    });
                }
                else
                {
                    toReturn.Concat(new ValidatorResult() { IsValid = false, Messages = new List<string>() { "The payload collection is empty" } });
                }
            }
            else
            {
                toReturn.Concat(new ValidatorResult() { IsValid = false, Messages = new List<string>() { "The payload is not a collection of items" } });
            }
            return toReturn;
        }
    }
}
