using Newtonsoft.Json.Linq;
using OTF.GwarWatcher.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty
{
    public abstract class CollectionPropertyValidator : PayloadPropertyValidatorBase, IPayloadPropertyValidator, IValidator<JObject>
    {
        protected abstract IEnumerable<IPayloadPropertyValidator> PayloadItemPropertyValidators { get; }
        public override ValidatorResult Validate(JObject payload)
        {
            ValidatorResult toReturn = new ValidatorResult() { IsValid = true, Messages = new List<string>() };

            if(payload != null)
            {
                JArray collection = payload.Property(this.PropertyName, StringComparison.InvariantCultureIgnoreCase)?.Value as JArray;
                if (collection != null && collection.Any())
                {
                    collection.ForEach(i => toReturn.Concat(this.ValidateItem(i as JObject)));
                }
            }
            return toReturn;
        }

        protected virtual ValidatorResult ValidateItem(JObject item)
        {
            ValidatorResult toReturn = new ValidatorResult() { IsValid = true, Messages = new List<string>() };
            if (item != null && this.PayloadItemPropertyValidators != null && this.PayloadItemPropertyValidators.Any())
            {
                this.PayloadItemPropertyValidators.ForEach(v => toReturn.Concat(v.Validate(item)));
            }
            return toReturn;
        }
    }
}
