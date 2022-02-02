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
            if (value is null)
            {
                writer.WriteNil();
            }

            Write(ref writer, value.Value);
        }

        private void Write(ref MessagePackWriter writer, TValue value)
        {
            if (typeof(TValue) == typeof(bool)) writer.Write((bool)(object)value);
            if (typeof(TValue) == typeof(byte)) writer.Write((byte)(object)value);
            if (typeof(TValue) == typeof(sbyte)) writer.Write((sbyte)(object)value);
            if (typeof(TValue) == typeof(short)) writer.Write((short)(object)value);
            if (typeof(TValue) == typeof(ushort)) writer.Write((ushort)(object)value);
            if (typeof(TValue) == typeof(int)) writer.Write((int)(object)value);
            if (typeof(TValue) == typeof(uint)) writer.Write((uint)(object)value);
            if (typeof(TValue) == typeof(long)) writer.Write((long)(object)value);
            if (typeof(TValue) == typeof(ulong)) writer.Write((ulong)(object)value);
            if (typeof(TValue) == typeof(float)) writer.Write((float)(object)value);
            if (typeof(TValue) == typeof(double)) writer.Write((double)(object)value);
            throw new ArgumentOutOfRangeException(nameof(value), $"{typeof(TValue)} is not supported."); // should not get to here
        }

        public TEnum Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if(reader.TryReadNil())
            {
                return default;
            }

            return SmartEnum<TEnum, TValue>.FromValue((TValue)Read(ref reader));
        }

        public TValue Read(ref MessagePackReader reader)
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