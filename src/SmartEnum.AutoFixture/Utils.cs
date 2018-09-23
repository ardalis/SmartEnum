using System;
using System.Collections;
using System.Reflection;

namespace Ardalis.SmartEnum.AutoFixture
{
    internal static class Utils
    {
        public static bool IsSmartEnum(Type type, out IEnumerable enums)
        {
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SmartEnum<,>))
                {
                    var listPropertyInfo = type.GetProperty("List", BindingFlags.Public | BindingFlags.Static);
                    enums = (IEnumerable)listPropertyInfo.GetValue(type, null);
                    return true;
                }
                type = type.BaseType;
            }

            enums = null;
            return false;
        }
    }
}