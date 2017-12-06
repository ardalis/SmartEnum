using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ardalis.GuardClauses;
using SmartEnum.Exceptions;

namespace Ardalis.SmartEnum
{
    /// <summary>
    /// A base type to use for creating smart enums in .NET.
    /// TEnum is the type that is inheriting from this class.
    /// TValue is the type of the enum value, typically int.
    /// </summary>
    /// <remarks></remarks>
    public abstract class SmartEnum<TEnum, TValue>
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

        public static List<TEnum> List
        {
            get
            {
                return _list.Value;
            }
        }

        public string Name { get; }
        public TValue Value { get; }

        protected SmartEnum(string name, TValue value)
        {
            Name = name;
            Value = value;
        }

        public static TEnum FromName(string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            var result = List.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            if(result == null)
            {
                throw new SmartEnumNotFoundException($"No option with Name \"{name}\" found.");
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
                throw new SmartEnumNotFoundException($"No option with Value {value} found.");
            }
            return result;
        }

        public override string ToString() => $"{Name} ({Value})";
    }
}
