﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty
{
    public abstract class Int32PropertyValidator : PayloadPropertyValidatorBase, IPayloadPropertyValidator, IValidator<JObject>
    {
        public override ValidatorResult Validate(JObject payload)
        {
            return payload.RunValidation(new List<(Func<JObject, bool> validation, Func<JObject, string> message)>()
            {
                { Rules.RequiredPropertyRule(this.PropertyName) },
                { Rules.RequiredPropertyTypeRule(this.PropertyName, JTokenType.Integer) },
                { Rules.Int32GreaterThanValueRule(this.PropertyName) }
            });
        }
    }
}
