using System.Linq;

namespace Ardalis.SmartEnum.Utf8Json
{
    using global::Utf8Json;
    using global::Utf8Json.Internal;
    using System;
    using Ardalis.SmartEnum;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmartFlagEnumNameFormatter<TEnum, TValue> : IJsonFormatter<TEnum>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        public void Serialize(ref JsonWriter writer, TEnum value, IJsonFormatterResolver formatterResolver)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(value.Name);
        }

        public TEnum Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            var name = reader.ReadString();
            return SmartFlagEnum<TEnum, TValue>.FromName(name).FirstOrDefault();
        }
    }
}