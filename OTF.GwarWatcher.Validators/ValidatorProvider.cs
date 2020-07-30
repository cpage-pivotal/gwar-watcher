using OTF.GwarWatcher.Validators.Core;
using OTF.GwarWatcher.Validators.Core.Attributes;
using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OTF.GwarWatcher.Common.Extensions;

namespace OTF.GwarWatcher.Validators
{
    public class ValidatorProvider
    {
        private IDictionary<int, IValidator<MessageModel>> _EventValidators = null;
        public IDictionary<int, IValidator<MessageModel>> EventValidators
        {
            get
            {
                if(this._EventValidators == null)
                {
                    this.RefreshEventValidators();
                }
                return this._EventValidators;
            }
        }

        private void RefreshEventValidators()
        {
            this._EventValidators = new Dictionary<int, IValidator<MessageModel>>();
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IValidator<MessageModel>).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(EventValidatorAttribute), true).Any())
                .ForEach(t =>
                {
                    EventValidatorAttribute attr = t.GetCustomAttributes(typeof(EventValidatorAttribute), true).FirstOrDefault() as EventValidatorAttribute;
                    if (attr != null)
                    {
                        this._EventValidators[attr.EventId] = (IValidator<MessageModel>)Activator.CreateInstance(t);
                    }
                });
        }

        public IValidator<MessageModel> GetValidator(int eventId) => this.EventValidators.ContainsKey(eventId) ? this.EventValidators[eventId] : null;
        public IValidator<MessageModel> GetValidator(MessageModel message) => message != null ? this.GetValidator(message.EventId) : null;
        public ValidatorResult Validate(MessageModel message) => this.GetValidator(message)?.Validate(message) ?? new ValidatorResult() { IsValid = false, Messages = new List<string>() { $"No validator for event id {message?.EventId}" } };
    }
}
