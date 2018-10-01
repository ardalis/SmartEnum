using Ardalis.GuardClauses;
using System;

namespace SmartEnum
{
    public static class SmartEnumExtensions
    {
        public static bool IsSmartEnum(this Type type) =>
            IsSmartEnum(type, out var _);

        public static bool IsSmartEnum(this Type type, out Type underlyingType)
        {
            Guard.Against.Null(type, nameof(type));

            if (type.IsAbstract || type.IsGenericTypeDefinition)
            {
                underlyingType = null;
                return false;
            }

            var baseType = type.BaseType;
            while (!(baseType is null))
            {
                if (baseType.IsClass && 
                    baseType.IsGenericType && 
                    baseType.GetGenericTypeDefinition() == typeof(Ardalis.SmartEnum.SmartEnum<,>))
                    {
                        underlyingType = baseType.GetGenericArguments()[1];
                        return true;
                    }

                baseType = baseType.BaseType;
            }
            underlyingType = null;
            return false;
        }
    }
}
