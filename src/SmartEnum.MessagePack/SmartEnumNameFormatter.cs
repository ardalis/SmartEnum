namespace Ardalis.SmartEnum.MessagePack
{
    using System;
    using global::MessagePack;
    using global::MessagePack.Formatters;

    public sealed class SmartEnumNameFormatter<TEnum, TValue> : IMessagePackFormatter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        public int Serialize(ref byte[] bytes, int offset, TEnum value, IFormatterResolver formatterResolver)
        {
            if (value is null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }

            return MessagePackBinary.WriteString(ref bytes, offset, value.Name);
        }

        public TEnum Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var name = MessagePackBinary.ReadString(bytes, offset, out readSize);
            return SmartEnum<TEnum, TValue>.FromName(name);
        }
    }
}