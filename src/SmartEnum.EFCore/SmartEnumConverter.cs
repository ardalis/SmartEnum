﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartEnum.EFCore;
using System;
using System.Reflection;

namespace Ardalis.SmartEnum.EFCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmartEnumConverter<TEnum, TValue> : ValueConverter<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        private static bool CanConvert(Type objectType)
        {
            return TypeUtil.IsDerived(objectType, typeof(SmartEnum<,>));
        }

        private static MethodInfo GetBaseFromValueMethod(Type objectType, Type valueType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return null;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(SmartEnum<,>))
                    return currentType.GetMethod("FromValue", new Type[] { valueType });

                currentType = currentType.BaseType;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static TEnum GetFromValue(TValue value)
        {
            if (!CanConvert(typeof(TEnum)))
            {
                throw new NotImplementedException();
            }

            var method = GetBaseFromValueMethod(typeof(TEnum), typeof(TValue));

            return method.Invoke(null, new[] { (object)value }) as TEnum;
        }

        /// <summary>
        /// 
        /// </summary>
        public SmartEnumConverter() : base(item => item.Value, key => GetFromValue(key), null)
        {
        }
    }
}