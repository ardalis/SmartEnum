namespace Ardalis.SmartEnum.MessagePack
{
    using System;
    using global::MessagePack;
    using global::MessagePack.Formatters;

    public sealed class SmartEnumNameFormatter<TEnum, TValue> : IMessagePackFormatter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TEnum value, MessagePackSerializerOptions options)
        {
            if (value is null) return;

            writer.Write(value.Name);
        }

        public TEnum Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var name = reader.ReadString();
            return SmartEnum<TEnum, TValue>.FromName(name);

        }
    }
}