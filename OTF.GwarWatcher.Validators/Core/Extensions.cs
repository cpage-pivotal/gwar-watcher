using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    internal static class Extensions
    {
        internal static ValidatorResult RunValidation<T>(this T input, IEnumerable<(Func<T, bool> validation, Func<T, string> message)> validations)
        {
            ValidatorResult toReturn = new ValidatorResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            if (input != null && validations != null && validations.Any())
            {
                toReturn.Messages = validations.Where(v => !v.validation(input)).Select(v => v.message(input));
                toReturn.IsValid = !toReturn.Messages.Any();
            }

            return toReturn;
        }

        internal static bool ContainsProperty(this JObject jo, string propertyName) => jo != null
            ? jo.Property(propertyName, StringComparison.InvariantCultureIgnoreCase) != null
            : false;

        internal static bool PropertyIsOfType(this JObject jo, string propertyName, params JTokenType[] types) => jo.ContainsProperty(propertyName)
            ? types.Contains(jo.GetPropertyType(propertyName))
            : false;

        internal static JTokenType GetPropertyType(this JObject jo, string propertyName) => jo.Property(propertyName, StringComparison.InvariantCultureIgnoreCase)?.Children().FirstOrDefault()?.Type ?? JTokenType.None;
        internal static string GetPropertyAsString(this JObject jo, string propertyName) => jo?.Property(propertyName, StringComparison.InvariantCultureIgnoreCase)?.Value.ToString() ?? string.Empty;
    }
}
