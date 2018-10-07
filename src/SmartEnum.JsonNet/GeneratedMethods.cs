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
        static readonly LockingConcurrentDictionary<Type, Func<string, object>> fromName = 
            new LockingConcurrentDictionary<Type, Func<string, object>>(enumType => CreateFromName(enumType));
        static readonly LockingConcurrentDictionary<Type, Func<object, object>> fromValue = 
            new LockingConcurrentDictionary<Type, Func<object, object>>(enumType => CreateFromValue(enumType));
        static readonly LockingConcurrentDictionary<Type, Action<JsonWriter, object>> writeValue = 
            new LockingConcurrentDictionary<Type, Action<JsonWriter, object>>(enumType => CreateWriteValue(enumType));

        public static object FromName(Type enumType, string name) =>
            fromName.GetOrAdd(enumType).Invoke(name);

        public static object FromValue(Type enumType, object value) =>
            fromValue.GetOrAdd(enumType).Invoke(value);

        public static void WriteValue(JsonWriter writer, object smartEnum) =>
            writeValue.GetOrAdd(smartEnum.GetType()).Invoke(writer, smartEnum);

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

        static Func<object, object> CreateFromValue(Type enumType)   
        {
            var valueType = enumType.GetValueType();

            var objectParam = Expression.Parameter(typeof(object));
            var value = Expression.Convert(objectParam, valueType);

            var methodInfo = enumType.GetMethod("FromValue", 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, 
                new Type[] { valueType }, null);
            var methodCall = Expression.Call(methodInfo, value);

            var lambda = Expression.Lambda<Func<object, object>>(methodCall, objectParam);
            return lambda.Compile();
        }

        // Avoids boxing of value
        static Action<JsonWriter, object> CreateWriteValue(Type enumType)   
        {
            var valueType = enumType.GetValueType();

            var objectParam = Expression.Parameter(typeof(object));
            var writerParam = Expression.Parameter(typeof(JsonWriter));

            var smartEnumType = typeof(ISmartEnum<>).MakeGenericType(valueType);
            var smartEnum = Expression.Convert(objectParam, smartEnumType);

            var getValueInfo = smartEnumType
                .GetProperty("Value", BindingFlags.Public | BindingFlags.Instance)
                .GetGetMethod();
            var getValueCall = Expression.Call(smartEnum, getValueInfo);

            var writeValueInfo = typeof(JsonWriter).GetMethod("WriteValue", 
                BindingFlags.Public | BindingFlags.Instance, null, 
                new Type[] { valueType }, null);
            var writeValueCall = Expression.Call(writerParam, writeValueInfo, getValueCall);

            var lambda = Expression.Lambda<Action<JsonWriter, object>>(writeValueCall, writerParam, objectParam);
            return lambda.Compile();
        }
    }
}