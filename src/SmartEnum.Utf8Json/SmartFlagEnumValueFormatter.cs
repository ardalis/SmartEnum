namespace Ardalis.SmartEnum.Utf8Json
{
    using global::Utf8Json;
    using global::Utf8Json.Internal;
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmartFlagEnumValueFormatter<TEnum, TValue> : IJsonFormatter<TEnum>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="formatterResolver"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Serialize(ref JsonWriter writer, TEnum value, IJsonFormatterResolver formatterResolver)
        {
            if (value is null)
            {
                writer.WriteNull();
            }
            else if (typeof(TValue) == typeof(byte))
                writer.WriteByte((byte)(object)value.Value);
            else if (typeof(TValue) == typeof(sbyte))
                writer.WriteSByte((sbyte)(object)value.Value);
            else if (typeof(TValue) == typeof(short))
                writer.WriteInt16((short)(object)value.Value);
            else if (typeof(TValue) == typeof(ushort))
                writer.WriteUInt16((ushort)(object)value.Value);
            else if (typeof(TValue) == typeof(int))
                writer.WriteInt32((int)(object)value.Value);
            else if (typeof(TValue) == typeof(uint))
                writer.WriteUInt32((uint)(object)value.Value);
            else if (typeof(TValue) == typeof(long))
                writer.WriteInt64((long)(object)value.Value);
            else if (typeof(TValue) == typeof(ulong))
                writer.WriteUInt64((ulong)(object)value.Value);
            else if (typeof(TValue) == typeof(float))
                writer.WriteSingle((float)(object)value.Value);
            else if (typeof(TValue) == typeof(double))
                writer.WriteDouble((double)(object)value.Value);
            else
                throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue).Name} is not supported.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="formatterResolver"></param>
        /// <returns></returns>
        public TEnum Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return SmartFlagEnum<TEnum, TValue>.DeserializeValue(ReadValue(ref reader));
        }

        TValue ReadValue(ref JsonReader reader)
        {
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
            throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue).Name} is not supported.");
        }
    }
}