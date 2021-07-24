using System.Globalization;
using System.Linq;

namespace Ardalis.SmartEnum
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class SmartEnumExtensions
    {
        public static bool IsSmartEnum(this Type type) =>
            IsSmartEnum(type, out var _);

        public static bool IsSmartEnum(this Type type, out Type[] genericArguments)
        {
            if (type is null || type.IsAbstract || type.IsGenericTypeDefinition)
            {
                genericArguments = null;
                return false;
            }

            do
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(SmartEnum<,>))
                {
                    genericArguments = type.GetGenericArguments();
                    return true;
                }

                type = type.BaseType;
            }
            while (!(type is null));

            genericArguments = null;
            return false;
        }

        public static bool TryGetValues(this Type type, out IEnumerable<object> enums)
        {
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SmartEnum<,>) || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SmartFlagEnum<,>))
                {
                    var listPropertyInfo = type.GetProperty("List", BindingFlags.Public | BindingFlags.Static);
                    enums = (IEnumerable<object>)listPropertyInfo.GetValue(type, null);
                    return true;
                }
                type = type.BaseType;
            }

            enums = null;
            return false;
        }
    }
}
