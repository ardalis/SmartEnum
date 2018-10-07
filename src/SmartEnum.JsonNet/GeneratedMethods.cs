using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using AutoMapper;

namespace Ardalis.SmartEnum.JsonNet
{
    static class GeneratedMethods
    {
        static readonly LockingConcurrentDictionary<Type, Func<string, ISmartEnum>> fromName = 
            new LockingConcurrentDictionary<Type, Func<string, ISmartEnum>>(enumType => CreateFromName(enumType));
        static readonly LockingConcurrentDictionary<Type, Func<object, ISmartEnum>> fromValue = 
            new LockingConcurrentDictionary<Type, Func<object, ISmartEnum>>(enumType => CreateFromValue(enumType));

        public static ISmartEnum FromName(Type enumType, string name) =>
            fromName.GetOrAdd(enumType).Invoke(name);

        public static ISmartEnum FromValue(Type enumType, object value) =>
            fromValue.GetOrAdd(enumType).Invoke(value);

        static Func<string, ISmartEnum> CreateFromName(Type enumType)   
        {
            var nameParam = Expression.Parameter(typeof(string));

            var fromNameInfo = enumType.GetMethod("FromName", 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, 
                new Type[] { typeof(string) }, null);
            var fromNameCall = Expression.Call(fromNameInfo, nameParam);
            
            var lambda = Expression.Lambda<Func<string, ISmartEnum>>(fromNameCall, nameParam);
            return lambda.Compile();
        }

        static Func<object, ISmartEnum> CreateFromValue(Type enumType)   
        {
            var valueType = enumType.GetValueType();

            var objectParam = Expression.Parameter(typeof(object));
            var value = Expression.Convert(objectParam, valueType);

            var fromValueInfo = enumType.GetMethod("FromValue", 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, 
                new Type[] { valueType }, null);
            var fromValueCall = Expression.Call(fromValueInfo, value);

            var lambda = Expression.Lambda<Func<object, ISmartEnum>>(fromValueCall, objectParam);
            return lambda.Compile();
        }
    }
}