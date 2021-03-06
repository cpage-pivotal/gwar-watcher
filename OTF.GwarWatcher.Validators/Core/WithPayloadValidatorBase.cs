﻿using OTF.GwarWatcher.Validators.Core.Message;
using OTF.GwarWatcher.Models;
using OTF.GwarWatcher.Validators.Core.PayloadProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace OTF.GwarWatcher.Validators.Core
{
    public abstract class WithPayloadValidatorBase<MT> : ValidatorBase<MT>, IValidator<MessageModel>
        where MT: IMessageValidator, new()
    {
        protected abstract IEnumerable<IPayloadPropertyValidator> PayloadPropertyValidators { get; }

        public override ValidatorResult Validate(MessageModel message)
        {
            ValidatorResult toReturn = base.Validate(message);
            toReturn.Concat(message.PayloadAsJObject != null
                ? this.PayloadPropertyValidators.Select(v => v.Validate(message.PayloadAsJObject))
                : new List<ValidatorResult>() { new ValidatorResult() { IsValid = false, Messages = new List<string>() { "There is no payload from this message" } } });
            return toReturn;
        }
    }
}
