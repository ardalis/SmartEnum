using Ardalis.GuardClauses;
using System;

namespace Ardalis.SmartEnum
{
    public static class SmartEnumExtensions
    {
        public static bool IsSmartEnum(this Type type)
        {
            if (type is null || type.IsAbstract || type.IsGenericTypeDefinition)
            {
                return false;
            }

            var enumType = type.GetInterface("ISmartEnum");
            if (enumType is null)
            {
                return false;
            }

            return true;        
        }

        public static bool IsSmartEnum(this Type type, out Type valueType)
        {
            if (type is null || type.IsAbstract || type.IsGenericTypeDefinition)
            {
                valueType = null;
                return false;
            }

            var enumType = type.GetInterface("ISmartEnum`1");
            if (enumType is null)
            {
                valueType = null;
                return false;
            }

            valueType = enumType.GetGenericArguments()[0];
            return true;
        }

        public static Type GetValueType(this Type type)
        {
            var enumType = type.GetInterface("ISmartEnum`1");
            if (enumType is null)
                throw new Exception($"{type.Name} is not a SmartEnum.");

            return enumType.GetGenericArguments()[0];
        }
    }
}
