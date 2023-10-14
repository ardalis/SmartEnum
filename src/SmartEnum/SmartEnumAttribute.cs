using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ardalis.SmartEnum
{
    /// <summary>
    /// A <see cref="ValidationAttribute"/> that ensures the provided value matches the
    /// <see cref="SmartEnum{TEnum}.Name"/> of a <see cref="SmartEnum{TEnum}"/>/<see cref="SmartEnum{TEnum,TValue}"/>.
    /// Nulls and non-<see cref="string"/> values are considered valid
    /// (add <see cref="RequiredAttribute"/> if you want the field to be required).
    /// </summary>
    public class SmartEnumAttribute : ValidationAttribute
    {
        private readonly bool _allowCaseInsensitiveMatch;
        private readonly Type _smartEnumType;

        /// <param name="smartEnumType">The expected SmartEnum type.</param>
        /// <param name="propertyName">The name of the property that the attribute is being used on.</param>
        /// <param name="allowCaseInsensitiveMatch">
        ///     Unless this is true, only exact case matching the
        ///     <see cref="SmartEnum{TEnum}" /> Name will validate.
        /// </param>
        /// <param name="errorMessage">
        ///     Message template to show when validation fails. {0} is <paramref name="propertyName" /> and
        ///     {1} is the comma-separated list of SmartEnum values.
        /// </param>
        /// <exception cref="ArgumentNullException">When any of the constructor parameters are null.</exception>
        /// <exception cref="InvalidOperationException">
        ///     When <paramref name="smartEnumType" /> is not a
        ///     <see cref="SmartEnum{TEnum}" /> or <see cref="SmartEnum{TEnum,TValue}" />
        /// </exception>
        public SmartEnumAttribute(Type smartEnumType, [CallerMemberName] string propertyName = null, bool allowCaseInsensitiveMatch = false,
            string errorMessage = "{0} must be one of: {1}")
        {
            if (smartEnumType is null) throw new ArgumentNullException(nameof(smartEnumType));
            if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
            if (errorMessage is null) throw new ArgumentNullException(nameof(errorMessage));
            List<string> smartEnumBaseTypes = new() { typeof(SmartEnum<>).Name, typeof(SmartEnum<,>).Name };
            if (smartEnumType.BaseType == null || !smartEnumBaseTypes.Contains(smartEnumType.BaseType.Name))
                throw new InvalidOperationException($"{nameof(smartEnumType)} must be a SmartEnum.");
            _smartEnumType = smartEnumType;
            _allowCaseInsensitiveMatch = allowCaseInsensitiveMatch;
            ErrorMessage = string.Format(errorMessage, propertyName, string.Join(", ", GetValidSmartEnumNames()));
        }

        public override bool IsValid(object value)
        {
            if (value is not string name) return true;

            return _allowCaseInsensitiveMatch
                ? GetValidSmartEnumNames().Contains(name, StringComparer.InvariantCultureIgnoreCase)
                : GetValidSmartEnumNames().Contains(name);
        }

        private List<string> GetValidSmartEnumNames()
        {
            List<string> values = new();
            var typeWithList = _smartEnumType.BaseType!.Name == typeof(SmartEnum<>).Name
                ? _smartEnumType.BaseType.BaseType!
                : _smartEnumType.BaseType!;
            var listProp = typeWithList.GetProperty("List", BindingFlags.Public | BindingFlags.Static);
            var rawValue = listProp!.GetValue(null);
            foreach (var val in (IEnumerable)rawValue!)
            {
                var namePropInfo = val.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
                var value = namePropInfo!.GetValue(val);
                if (value is string valName) values.Add(valName);
            }
            return values;
        }
    }
}
