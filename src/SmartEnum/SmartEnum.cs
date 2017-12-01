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
    public abstract class SmartEnum<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
    {
        private static readonly List<TEnum> _list = new List<TEnum>();

        // Despite analysis tool warnings, we want this static bool to be on this generic type 
        // (so that each TEnum has its own bool).
        private static bool _invoked;

        public static List<TEnum> List
        {
            get
            {
                if (!_invoked)
                {
                    _invoked = true;
                    // Force invocation/initialization by calling one of the derived members.
                    typeof(TEnum).GetProperties(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(p => p.PropertyType == typeof(TEnum))?.GetValue(null, null);
                }

                return _list;
            }
        }

        public string Name { get; }
        public TValue Value { get; }

        protected SmartEnum(string name, TValue value)
        {
            Name = name;
            Value = value;

            TEnum item = this as TEnum;
            List.Add(item);
        }

        public static TEnum FromName(string name)
        {
            return List.Single(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public static TEnum FromValue(TValue value)
        {
            // Can't use == to compare generics unless we constrain TValue to "class", 
            // which we don't want because then we couldn't use int.
            return List.Single(item => EqualityComparer<TValue>.Default.Equals(item.Value, value));
        }

        public override string ToString() => $"{Name} ({Value})";
    }
}
