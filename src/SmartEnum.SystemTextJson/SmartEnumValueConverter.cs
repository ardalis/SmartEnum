namespace Ardalis.SmartEnum.SystemTextJson
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmartEnumValueConverter<TEnum, TValue> : JsonConverter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IConvertible
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            return GetFromValue(ReadValue(ref reader));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else if (typeof(TValue) == typeof(bool))
                writer.WriteBooleanValue((bool)(object)value.Value);
            else if (typeof(TValue) == typeof(short))
                writer.WriteNumberValue((int)(short)(object)value.Value);
            else if (typeof(TValue) == typeof(int))
                writer.WriteNumberValue((int)(object)value.Value);
            else if (typeof(TValue) == typeof(double))
                writer.WriteNumberValue((double)(object)value.Value);
            else if (typeof(TValue) == typeof(decimal))
                writer.WriteNumberValue((decimal)(object)value.Value);
            else if (typeof(TValue) == typeof(ulong))
                writer.WriteNumberValue((ulong)(object)value.Value);
            else if (typeof(TValue) == typeof(uint))
                writer.WriteNumberValue((uint)(object)value.Value);
            else if (typeof(TValue) == typeof(float))
                writer.WriteNumberValue((float)(object)value.Value);
            else if (typeof(TValue) == typeof(long))
                writer.WriteNumberValue((long)(object)value.Value);
            else
                writer.WriteStringValue(value.Value.ToString());
        }

        private TEnum GetFromValue(TValue value)
        {
            try
            {
                return SmartEnum<TEnum, TValue>.FromValue(value);
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error converting value '{value}' to a smart enum.", ex);
            }
        }

        private TValue ReadValue(ref Utf8JsonReader reader)
        {
            if (typeof(TValue) == typeof(bool))
                return (TValue)(object)reader.GetBoolean();
            if (typeof(TValue) == typeof(byte))
                return (TValue)(object)reader.GetByte();
            if (typeof(TValue) == typeof(sbyte))
                return (TValue)(object)reader.GetSByte();
            if (typeof(TValue) == typeof(short))
                return (TValue)(object)reader.GetInt16();
            if (typeof(TValue) == typeof(ushort))
                return (TValue)(object)reader.GetUInt16();
            if (typeof(TValue) == typeof(int))
                return (TValue)(object)reader.GetInt32();
            if (typeof(TValue) == typeof(uint))
                return (TValue)(object)reader.GetUInt32();
            if (typeof(TValue) == typeof(long))
                return (TValue)(object)reader.GetInt64();
            if (typeof(TValue) == typeof(ulong))
                return (TValue)(object)reader.GetUInt64();
            if (typeof(TValue) == typeof(float))
                return (TValue)(object)reader.GetSingle();
            if (typeof(TValue) == typeof(double))
                return (TValue)(object)reader.GetDouble();
            if (typeof(TValue) == typeof(string))
                return (TValue)(object)reader.GetString();

            throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue).Name} is not supported.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public override TEnum ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new ArgumentException(
                    $"Unexpected token {reader.TokenType} when parsing a dictionary with smart enum key.");
            }
            return GetFromValue(ReadPropertyName(ref reader));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void WriteAsPropertyName(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WritePropertyName(value.Value?.ToString() ?? string.Empty);
        }

        private TValue ReadPropertyName(ref Utf8JsonReader reader)
        {
            if (typeof(TValue) == typeof(bool))
                return (TValue)(object)Convert.ToBoolean(reader.GetString());
            if (typeof(TValue) == typeof(byte))
                return (TValue)(object)Convert.ToByte(reader.GetString());
            if (typeof(TValue) == typeof(sbyte))
                return (TValue)(object)Convert.ToSByte(reader.GetString());
            if (typeof(TValue) == typeof(short))
                return (TValue)(object)Convert.ToInt16(reader.GetString());
            if (typeof(TValue) == typeof(ushort))
                return (TValue)(object)Convert.ToUInt16(reader.GetString());
            if (typeof(TValue) == typeof(int))
                return (TValue)(object)Convert.ToInt32(reader.GetString());
            if (typeof(TValue) == typeof(uint))
                return (TValue)(object)Convert.ToUInt32(reader.GetString());
            if (typeof(TValue) == typeof(long))
                return (TValue)(object)Convert.ToInt64(reader.GetString());
            if (typeof(TValue) == typeof(ulong))
                return (TValue)(object)Convert.ToUInt64(reader.GetString());
            if (typeof(TValue) == typeof(float))
                return (TValue)(object)Convert.ToSingle(reader.GetString());
            if (typeof(TValue) == typeof(double))
                return (TValue)(object)Convert.ToDouble(reader.GetString());
            if (typeof(TValue) == typeof(string))
                return (TValue)(object)reader.GetString();

            throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue).Name} is not supported.");
        }
    }
}