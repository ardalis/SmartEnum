using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Ardalis.SmartEnum.JsonNet
{
    static class GeneratedMethods
    {
        static readonly ConcurrentDictionary<Type, Func<string, object>> fromName = 
            new ConcurrentDictionary<Type, Func<string, object>>();
        static readonly ConcurrentDictionary<Type, Func<object, object>> fromValue = 
            new ConcurrentDictionary<Type, Func<object, object>>();

        public static Func<string, object> FromName(Type enumType) =>
            fromName.GetOrAdd(enumType, _ => CreateFromName(enumType));

        public static Func<object, object> FromValue(Type enumType, Type valueType) =>
            fromValue.GetOrAdd(enumType, _ => CreateFromValue(enumType, valueType));

        static Func<string, object> CreateFromName(Type enumType)   
        {
            var nameParam = Expression.Parameter(typeof(string));

            var methodInfo = enumType.GetMethod("FromName", 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, 
                new Type[] { typeof(string) }, null);
            var methodCall = Expression.Call(methodInfo, nameParam);
            
            var lambda = Expression.Lambda<Func<string, object>>(methodCall, nameParam);
            return lambda.Compile();
        }

        static Func<object, object> CreateFromValue(Type enumType, Type valueType)   
        {
            var objectParam = Expression.Parameter(typeof(object));
            var value = Expression.Convert(objectParam, valueType);

            var methodInfo = enumType.GetMethod("FromValue", 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, 
                new Type[] { valueType }, null);
            var methodCall = Expression.Call(methodInfo, value);

            var lambda = Expression.Lambda<Func<object, object>>(methodCall, objectParam);
            return lambda.Compile();
        }
    }
}