using System;
using System.Runtime.Serialization;

namespace Ardalis.SmartEnum.Serialization
{
    public class SmartEnumSurrogateProvider<TEnum, TValue> : ISerializationSurrogateProvider
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        public Type GetSurrogateType(Type type)
        {
            if (type is TEnum)
                return typeof(string);

            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (obj is null)
                return null;

            var name = (string)obj;
            if(!SmartEnum<TEnum, TValue>.TryFromName(name, out var result))
                throw new SerializationException($"Error converting value '{name}' to a smart enum.");

            return result;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is null)
                return null;

            var smartEnum = (SmartEnum<TEnum, TValue>)obj;
            return smartEnum.Name;
        }
    }
}
