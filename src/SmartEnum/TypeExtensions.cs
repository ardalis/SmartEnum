using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ardalis.SmartEnum;

internal static class TypeExtensions
{
    private static readonly ConcurrentDictionary<Type, object> FieldCache = new();
    public static List<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
    {
        return (List<TFieldType>)FieldCache.GetOrAdd(type, t =>
        {
            return t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => t.IsAssignableFrom(p.FieldType))
                .Select(pi => (TFieldType)pi.GetValue(null))
                .ToList();
        });
    }
}
