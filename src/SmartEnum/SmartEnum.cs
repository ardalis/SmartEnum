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
            Type baseType = typeof(TEnum);
            IEnumerable<Type> enumTypes = Assembly.GetAssembly(baseType).GetTypes().Where(t => baseType.IsAssignableFrom(t));

            List<TEnum> options = new List<TEnum>();
            foreach (Type enumType in enumTypes)
            {
                List<TEnum> typeEnumOptions = enumType.GetFieldsOfType<TEnum>();
                options.AddRange(typeEnumOptions);
            }

            return options.OrderBy(t => t.Name).ToList();
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
