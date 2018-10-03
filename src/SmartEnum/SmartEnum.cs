using Ardalis.GuardClauses;
using SmartEnum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ardalis.SmartEnum
{
    /// <summary>
    /// A base type to use for creating smart enums in .NET.
    /// </summary>
    /// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
    /// <typeparam name="TValue">The type of the enum value, typically <see cref="System.Int32"/>.</typeparam>
    /// <remarks></remarks>
    public abstract class SmartEnum<TEnum, TValue> : IEquatable<SmartEnum<TEnum, TValue>>
        where TEnum : SmartEnum<TEnum, TValue>
    {
        private static readonly Lazy<Dictionary<string, TEnum>> _fromName = 
            new Lazy<Dictionary<string, TEnum>>(() => ListAllOptions().ToDictionary(item => item.Name));

        private static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase = 
            new Lazy<Dictionary<string, TEnum>>(() => ListAllOptions().ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

        private static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue = 
            new Lazy<Dictionary<TValue, TEnum>>(() => {
                // multiple enums with same value are allowed but store only one per value
                var dictionary = new Dictionary<TValue, TEnum>();
                foreach (var item in ListAllOptions())
                {
                    if (!dictionary.ContainsKey(item.Value))
                        dictionary.Add(item.Value, item);
                }
                return dictionary;
            });

        private static IEnumerable<TEnum> ListAllOptions()
        {
            Type type = typeof(TEnum);
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fieldInfo => type.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo => (TEnum)fieldInfo.GetValue(null));
        }

        public static IReadOnlyCollection<TEnum> List => _fromName.Value.Values;

        public string Name { get; }

        public TValue Value { get; protected set; }
        protected SmartEnum(string name, TValue value)
        {
            Name = name;
            Value = value;
        }

        protected SmartEnum()
        {
            // Required for EF
        }

        public Type GetUnderlyingType () => typeof(TValue);

        public static TEnum FromName(string name, bool ignoreCase = true)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            if (ignoreCase)
                return FromName(_fromNameIgnoreCase.Value);
            else
                return FromName(_fromName.Value);

            TEnum FromName(Dictionary<string, TEnum> dictionary)
            {
                if (!dictionary.TryGetValue(name, out TEnum result))
                {
                    throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Name \"{name}\" found.");
                }
                return result;
            }
        }

        public static bool TryFromName(string name, out TEnum result) =>
            TryFromName(name, true, out result);

        public static bool TryFromName(string name, bool ignoreCase, out TEnum result)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            if (ignoreCase)
                return _fromNameIgnoreCase.Value.TryGetValue(name, out result);
            else
                return _fromName.Value.TryGetValue(name, out result);
        }

        public static TEnum FromValue(TValue value)
        {
            if (!_fromValue.Value.TryGetValue(value, out TEnum result))
            {
                throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");
            }
            return result;
        }

        public static TEnum FromValue(TValue value, TEnum defaultValue)
        {
            if (!_fromValue.Value.TryGetValue(value, out TEnum result))
            {
                return defaultValue;
            }
            return result;
        }

        public static bool TryFromValue(TValue value, out TEnum result) =>
            _fromValue.Value.TryGetValue(value, out result);

        public override string ToString() => $"{Name} ({Value})";
        public override int GetHashCode() => ReferenceEquals(null, Value) ? 0 : Value.GetHashCode(); 
        public override bool Equals(object obj) => (obj is SmartEnum<TEnum, TValue> other) && Equals(other);

        public bool Equals(SmartEnum<TEnum, TValue> other)
        {
            if (other is null)
            {
                return false;
            }

            // If the objects share the same reference, return true
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Return true if value matches
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public static bool operator ==(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right)
        {
            // Handle null on left side
            if (left is null)
            {
                // null == null = true
                return right is null;
            }

            // Equals handles null on right side
            return left.Equals(right);
        }

        public static bool operator !=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) => !(left == right);

        public static implicit operator TValue(SmartEnum<TEnum, TValue> smartEnum) => smartEnum.Value;
        public static explicit operator SmartEnum<TEnum, TValue>(TValue value) => FromValue(value);
    }
}