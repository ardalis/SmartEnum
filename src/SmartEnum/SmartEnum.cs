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
    /// TEnum is the type that is inheriting from this class.
    /// TValue is the type of the enum value, typically int.
    /// </summary>
    /// <remarks></remarks>
    public abstract class SmartEnum<TEnum, TValue> : IEquatable<SmartEnum<TEnum, TValue>>
        where TEnum : SmartEnum<TEnum, TValue>
    {
        private static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue = 
            new Lazy<Dictionary<TValue, TEnum>>(() => AsDictionary(ListAllOptions(), i => i.Value));

        private static readonly Lazy<Dictionary<string, TEnum>> _fromName = 
            new Lazy<Dictionary<string, TEnum>>(() => AsDictionary(ListAllOptions(), i => i.Name, StringComparer.OrdinalIgnoreCase));

        private static IOrderedEnumerable<TEnum> ListAllOptions()
        {
            Type t = typeof(TEnum);
            return t.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(p => t.IsAssignableFrom(p.FieldType))
            .Select(pi => (TEnum)pi.GetValue(null))
            .OrderBy(p => p.Name);
        }

        private static Dictionary<TKey, TEnum> AsDictionary<TKey>(IEnumerable<TEnum> enums, 
            Func<TEnum, TKey> keySelector,
            IEqualityComparer<TKey> comparer = null)
        {
            var dictionary = new Dictionary<TKey, TEnum>(comparer);
            foreach(var item in enums)
            {
                var key = keySelector(item);
                if(dictionary.ContainsKey(key))
                {
                    throw new SmartEnumDuplicateException(typeof(TKey), key);
                }
                dictionary.Add(key, item);
            }
            return dictionary;
        }

        public static IReadOnlyCollection<TEnum> List => _fromValue.Value.Values;

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

        public static TEnum FromName(string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            if (!_fromName.Value.TryGetValue(name, out var result))
            {
                throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Name \"{name}\" found.");
            }
            return result;
        }

        public static TEnum FromValue(TValue value)
        {
            if (!_fromValue.Value.TryGetValue(value, out var result))
            {
                throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");
            }
            return result;
        }

        public static TEnum FromValue(TValue value, TEnum defaultValue)
        {
            if (!_fromValue.Value.TryGetValue(value, out var result))
            {
                result = defaultValue;
            }
            return result;
        }

        public override string ToString() => $"{Name} ({Value})";
        public override int GetHashCode() => new { Name, Value }.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as SmartEnum<TEnum, TValue>);

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

            // If the runtime types are not the same, return false
            if (GetType() != other.GetType())
            {
                return false;
            }

            // Return true if both name and value match
            return Name == other.Name && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public static bool operator ==(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right)
        {
            // Handle null on left side
            if (left is null)
            {
                if (right is null)
                {
                    // null == null = true
                    return true;
                }

                return false;
            }

            // Equals handles null on right side
            return left.Equals(right);
        }

        public static bool operator !=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) => !(left == right);

        public static implicit operator TValue(SmartEnum<TEnum, TValue> smartEnum) => smartEnum.Value;
        public static explicit operator SmartEnum<TEnum, TValue>(TValue value) => FromValue(value);
    }
}