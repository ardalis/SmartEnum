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
            if (type is null || type.IsAbstract || type.IsGenericTypeDefinition)
            {
                valueType = null;
                return false;
            }

            do
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(Ardalis.SmartEnum.SmartEnum<,>))
                {
                    valueType = type.GetGenericArguments()[1];
                    return true;
                }

                type = type.BaseType;
            }
            while (!(type is null));

            valueType = null;
            return false;
        }
    }
}
