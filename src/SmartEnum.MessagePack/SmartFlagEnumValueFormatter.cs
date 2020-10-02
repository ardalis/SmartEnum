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
    public class SmartFlagEnumValueFormatter<TEnum, TValue> : IMessagePackFormatter<TEnum>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="formatterResolver"></param>
        /// <returns></returns>
        public int Serialize(ref byte[] bytes, int offset, TEnum value, IFormatterResolver formatterResolver)
        {
            if (value is null)
                return MessagePackBinary.WriteNil(ref bytes, offset);

            return Write(ref bytes, offset, value.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="formatterResolver"></param>
        /// <param name="readSize"></param>
        /// <returns></returns>
        public TEnum Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return default;
            }

            return SmartFlagEnum<TEnum, TValue>.DeserializeValue(Read(ref bytes, offset, out readSize));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Write(ref byte[] bytes, int offset, TValue value)
        {
            if (typeof(TValue) == typeof(byte))
                return MessagePackBinary.WriteByte(ref bytes, offset, (byte)(object)value);
            if (typeof(TValue) == typeof(sbyte))
                return MessagePackBinary.WriteSByte(ref bytes, offset, (sbyte)(object)value);
            if (typeof(TValue) == typeof(short))
                return MessagePackBinary.WriteInt16(ref bytes, offset, (short)(object)value);
            if (typeof(TValue) == typeof(ushort))
                return MessagePackBinary.WriteUInt16(ref bytes, offset, (ushort)(object)value);
            if (typeof(TValue) == typeof(int))
                return MessagePackBinary.WriteInt32(ref bytes, offset, (int)(object)value);
            if (typeof(TValue) == typeof(uint))
                return MessagePackBinary.WriteUInt32(ref bytes, offset, (uint)(object)value);
            if (typeof(TValue) == typeof(long))
                return MessagePackBinary.WriteInt64(ref bytes, offset, (long)(object)value);
            if (typeof(TValue) == typeof(ulong))
                return MessagePackBinary.WriteUInt64(ref bytes, offset, (ulong)(object)value);
            if (typeof(TValue) == typeof(float))
                return MessagePackBinary.WriteSingle(ref bytes, offset, (float)(object)value);
            if (typeof(TValue) == typeof(double))
                return MessagePackBinary.WriteDouble(ref bytes, offset, (double)(object)value);
            throw new ArgumentOutOfRangeException(nameof(value), $"{typeof(TValue)} is not supported."); // should not get to here
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="readSize"></param>
        /// <returns></returns>
        public TValue Read(ref byte[] bytes, int offset, out int readSize)
        {
            if (typeof(TValue) == typeof(byte))
                return (TValue)(object)MessagePackBinary.ReadByte(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(sbyte))
                return (TValue)(object)MessagePackBinary.ReadSByte(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(short))
                return (TValue)(object)MessagePackBinary.ReadInt16(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(ushort))
                return (TValue)(object)MessagePackBinary.ReadUInt16(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(int))
                return (TValue)(object)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(uint))
                return (TValue)(object)MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(long))
                return (TValue)(object)MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(ulong))
                return (TValue)(object)MessagePackBinary.ReadUInt64(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(float))
                return (TValue)(object)MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            if (typeof(TValue) == typeof(double))
                return (TValue)(object)MessagePackBinary.ReadDouble(bytes, offset, out readSize);
            throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue)} is not supported."); // should not get to here
        }
    }
}