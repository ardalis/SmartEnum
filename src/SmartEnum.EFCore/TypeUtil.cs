using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnum.EFCore
{
    internal static class TypeUtil
    {
        public static bool IsDerived(Type objectType, Type mainType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return false;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == mainType)
                    return true;

                currentType = currentType.BaseType;
            }

            return false;
        }

        public static Type GetEnumType(Type objectType, Type mainType)
        {
            return GetEnumAndValueTypes(objectType, mainType).EnumType;
        }

        public static Type GetValueType(Type objectType, Type mainType)
        {
            return GetEnumAndValueTypes(objectType, mainType).ValueType;
        }

        public static (Type EnumType, Type ValueType) GetEnumAndValueTypes(Type objectType, Type mainType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return (null, null);
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == mainType)
                    return (currentType.GenericTypeArguments[0], currentType.GenericTypeArguments[1]);

                currentType = currentType.BaseType;
            }

            return (null, null);
        }
    }
}