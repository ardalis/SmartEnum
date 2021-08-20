namespace Ardalis.SmartEnum.MessagePack
{
    using System;
    using global::MessagePack;
    using global::MessagePack.Formatters;

    public class SmartEnumValueFormatter<TEnum, TValue> : IMessagePackFormatter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TEnum value, MessagePackSerializerOptions options)
        {
            if(value is null)
            {
                writer.WriteNil();
            }
            else
            {
                Write(writer, value.Value);
            }
        }

        public TEnum Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return default;
            }

            return SmartEnum<TEnum, TValue>.FromValue(Read(reader));
        }

        //public int Serialize(ref byte[] bytes, int offset, TEnum value, IFormatterResolver formatterResolver)
        //{
        //    if (value is null)
        //        return MessagePackBinary.WriteNil(ref bytes, offset);

        //    return Write(ref bytes, offset, value.Value);
        //}

        //public TEnum Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        //{
        //    if (MessagePackBinary.IsNil(bytes, offset))
        //    {
        //        readSize = 1;
        //        return default;
        //    }

        //    return SmartEnum<TEnum, TValue>.FromValue(Read());
        //}

        private void Write(MessagePackWriter writer, TValue value)
        {
            switch(value)
            {
                case bool boolean:
                    writer.Write(boolean);
                    break;
                case byte myByte:
                    writer.Write(myByte);
                    break;
                case sbyte mysByte:
                    writer.Write(mysByte);
                    break;
                case short myShort:
                    writer.Write(myShort);
                    break;
                case ushort myUShort:
                    writer.Write(myUShort);
                    break;
                case int myInt:
                    writer.Write(myInt);
                    break;
                case uint myUInt:
                    writer.Write(myUInt);
                    break;
                case long myLong:
                    writer.Write(myLong);
                    break;
                case ulong myULong:
                    writer.Write(myULong);
                    break;
                case float myFloat:
                    writer.Write(myFloat);
                    break;
                case double myDouble:
                    writer.Write(myDouble);
                    break;

                case object _:
                    throw new ArgumentOutOfRangeException(nameof(value), $"{typeof(TValue)} is not supported."); // should not get to here
            }
        }

        private TValue Read(MessagePackReader reader)
        {
            if (typeof(TValue) == typeof(bool))
                return (TValue)(object)reader.ReadBoolean();
            if (typeof(TValue) == typeof(byte))
                return (TValue)(object)reader.ReadByte();
            if (typeof(TValue) == typeof(sbyte))
                return (TValue)(object)reader.ReadSByte();
            if (typeof(TValue) == typeof(short))
                return (TValue)(object)reader.ReadInt16();
            if (typeof(TValue) == typeof(ushort))
                return (TValue)(object)reader.ReadUInt16();
            if (typeof(TValue) == typeof(int))
                return (TValue)(object)reader.ReadInt32();
            if (typeof(TValue) == typeof(uint))
                return (TValue)(object)reader.ReadUInt32();
            if (typeof(TValue) == typeof(long))
                return (TValue)(object)reader.ReadInt64();
            if (typeof(TValue) == typeof(ulong))
                return (TValue)(object)reader.ReadUInt64();
            if (typeof(TValue) == typeof(float))
                return (TValue)(object)reader.ReadSingle();
            if (typeof(TValue) == typeof(double))
                return (TValue)(object)reader.ReadDouble();
            throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue)} is not supported."); // should not get to here
        }
    }
}