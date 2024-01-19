using System.Linq;

namespace Ardalis.SmartEnum.MessagePack
{
    using System;
    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class SmartFlagEnumNameFormatter<TEnum, TValue> : IMessagePackFormatter<TEnum>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public void Serialize(ref MessagePackWriter writer, TEnum value, MessagePackSerializerOptions options)
        {
            if (value is null) return;

            writer.Write(value.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TEnum Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var name = reader.ReadString();
            return SmartFlagEnum<TEnum, TValue>.FromName(name).FirstOrDefault();
        }
    }
}


