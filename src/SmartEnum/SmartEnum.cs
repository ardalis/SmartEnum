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
        private static readonly Lazy<List<TEnum>> _list = new Lazy<List<TEnum>>(ListAllOptions);

        private static List<TEnum> ListAllOptions()
        {
            Type t = typeof(TEnum);
            return t.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(p => t.IsAssignableFrom(p.FieldType))
            .Select(pi => (TEnum)pi.GetValue(null))
            .OrderBy(p => p.Name)
            .ToList();
        }

        public static List<TEnum> List => _list.Value;

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
            var result = List.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            if (result == null)
            {
                throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Name \"{name}\" found.");
            }
            return result;
        }

        public static TEnum FromValue(TValue value)
        {
            // Can't use == to compare generics unless we constrain TValue to "class",
            // which we don't want because then we couldn't use int.
            var result = List.FirstOrDefault(item => EqualityComparer<TValue>.Default.Equals(item.Value, value));
            if (result == null)
            {
                throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");
            }
            return result;
        }

        public static TEnum FromValue(TValue value, TEnum defaultValue)
        {
            // Can't use == to compare generics unless we constrain TValue to "class",
            // which we don't want because then we couldn't use int.
            var result = List.FirstOrDefault(item => EqualityComparer<TValue>.Default.Equals(item.Value, value));
            if (result == null)
            {
                result = defaultValue;
            }
            return result;
        }

        public override string ToString() => $"{Name} ({Value})";
        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                var hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (Object.ReferenceEquals(null, Value) ? 0 : Value.GetHashCode());
                hash = (hash * HashingMultiplier) ^ (Name is null ? 0 : Name.GetHashCode());
                return hash;
            }
        }   

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

            // Return true if both name and value match
            return Name == other.Name && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public static bool operator ==(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right)
        {
            // Handle null on left side
            if (left is null)
            {
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