using Ardalis.GuardClauses;
using System;

namespace SmartEnum
{
    public static class SmartEnumExtensions
    {
        public static bool IsSmartEnum(this Type type) =>
            IsSmartEnum(type, out var _);

        public static bool IsSmartEnum(this Type type, out Type valueType)
        {
            Guard.Against.Null(type, nameof(type));

            if (type.IsAbstract || type.IsGenericTypeDefinition)
            {
                valueType = null;
                return false;
            }

            var baseType = type.BaseType;
            while (!(baseType is null))
            {
                if (baseType.IsClass && 
                    baseType.IsGenericType && 
                    baseType.GetGenericTypeDefinition() == typeof(Ardalis.SmartEnum.SmartEnum<,>))
                    {
                        valueType = baseType.GetGenericArguments()[1];
                        return true;
                    }

                baseType = baseType.BaseType;
            }
            valueType = null;
            return false;
        }
    }
}
