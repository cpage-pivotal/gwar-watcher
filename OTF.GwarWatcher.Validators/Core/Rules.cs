using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    internal static class Rules
    {
        #region JObject Rules
        internal static (Func<JObject, bool> validation, Func<JObject, string> message) RequiredPropertyRule(string propertyName) =>
            (validation: jo => jo.ContainsProperty(propertyName) && !jo.PropertyIsOfType(propertyName, JTokenType.Null), message: jo => $"{propertyName} is missing or null");
        internal static (Func<JObject, bool> validation, Func<JObject, string> message) RequiredPropertyTypeRule(string propertyName, params JTokenType[] types) =>
            (validation: jo => jo.PropertyIsOfType(propertyName, types), message: jo => $"{propertyName} is not of the appropriate type ({string.Join(", ", types.Select(t => t.ToString()))}) - it is of type {jo.GetPropertyType(propertyName)}");
        internal static (Func<JObject, bool> validation, Func<JObject, string> message) Int32GreaterThanValueRule(string propertyName, int minValue = 0) =>
            (validation: jo => ((int?)jo.Property(propertyName, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault(minValue) > minValue, message: jo => $"{propertyName} is not greater than {minValue}");
        internal static (Func<JObject, bool> validation, Func<JObject, string> message) FloatGreaterThanValueRule(string propertyName, float minValue = 0) =>
            (validation: jo => ((float?)jo.Property(propertyName, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault(minValue) > minValue, message: jo => $"{propertyName} is not greater than {minValue}");
        internal static (Func<JObject, bool> validation, Func<JObject, string> message) StringIsNotEmpty(string propertyName) =>
            (validation: jo => !string.IsNullOrEmpty(jo.GetPropertyAsString(propertyName)), message: jo => $"{propertyName} is null or empty");
        #endregion

        #region MessageModel Rules
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) CategoryValueRule(string category, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => string.Equals((m.Category ?? string.Empty), category, comparison), message: m => $"Category was not set to \"{category}\", it was set to \"{m.Category}\"");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) CategoryValueRule(IEnumerable<string> categories, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => categories.Any(c => string.Equals((m.Category ?? string.Empty), c, comparison)), message: m => $"Category was not set to one of these values: \"{string.Join("\", \"", categories)}\", it was set to \"{m.Category}\"");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) ActionValueRule(string action, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => string.Equals((m.Action ?? string.Empty), action, comparison), message: m => $"Action was not set to \"{action}\", it was set to \"{m.Action}\"");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) ValueRule(string value, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => string.Equals(m.Value, value, comparison), message: m => $"Value is not set to \"{value}\", it was set to {m.Value}");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) ValuePayloadPropertyRule(string payloadProperty, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => string.Equals(m.Value, m.PayloadAsJObject?.GetPropertyAsString(payloadProperty) ?? string.Empty, comparison), message: m => $"Value is not set to {payloadProperty}, it was set to \"{m.Value}\"");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) LabelValueRule(string label, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => string.Equals(m.Label, label, comparison), message: m => $"Label is not set to \"{label}\", it was set to {m.Label}");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) LabelPayloadPropertyRule(string payloadProperty, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => string.Equals(m.Label, m.PayloadAsJObject?.GetPropertyAsString(payloadProperty) ?? string.Empty, comparison), message: m => $"Label is not set to {payloadProperty}, it was set to \"{m.Label}\"");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) ActionValueRule(IEnumerable<string> actions, StringComparison comparison = StringComparison.CurrentCulture) =>
            (validation: m => actions.Any(a => string.Equals((m.Action ?? string.Empty), a, comparison)), message: m => $"ACtion was not set to one of these values: \"{string.Join("\", \"", actions)}\", it was set to \"{m.Action}\"");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) RequiredPropertyRule<T>(Func<Models.MessageModel, T> getValue, string propertyDiscription) =>
            (validation: m => getValue(m) != null, message: m => $"{propertyDiscription} is missing or null");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) RequiredStringPropertyRule(Func<Models.MessageModel, string> getValue, string propertyDiscription) =>
            (validation: m => !string.IsNullOrEmpty(getValue(m)), message: m => $"{propertyDiscription} is missing or null");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) RequiredInt32PropertyRule(Func<Models.MessageModel, int> getValue, string propertyDiscription, int minValue = 0) =>
            (validation: m => getValue(m) > minValue, message: m => $"{propertyDiscription} is missing, null or set something not greater than {minValue}");
        internal static (Func<Models.MessageModel, bool> validation, Func<Models.MessageModel, string> message) MessageFieldShouldEqualPayloadProperty(Func<Models.MessageModel, string> getMessageField, string messageFieldName, string propertyName) =>
            (validation: m => string.Equals(getMessageField(m), m.PayloadAsJObject?.Property(propertyName, StringComparison.InvariantCultureIgnoreCase)?.Value.ToString() ?? string.Empty, StringComparison.InvariantCultureIgnoreCase), message: m => $"{messageFieldName} is not set to {propertyName}, {messageFieldName}: {getMessageField(m)}, {propertyName}: {m.PayloadAsJObject?.GetPropertyAsString(propertyName)}");
        #endregion
    }
}
