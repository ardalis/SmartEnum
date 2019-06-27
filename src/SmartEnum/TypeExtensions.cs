using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ardalis.SmartEnum
{
    internal static class TypeExtensions
    {
        public static List<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => type.IsAssignableFrom(p.FieldType))
                .Select(pi => (TFieldType)pi.GetValue(null))
                .ToList();
        }
    }
}
