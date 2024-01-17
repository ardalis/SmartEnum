namespace Ardalis.SmartEnum.SystemTextJson
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmartFlagEnumNameConverter<TEnum, TValue> : JsonConverter<TEnum> 
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : struct, IComparable<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        public override bool HandleNull => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return GetFromName(reader.GetString());

                default:
                    throw new JsonException($"Unexpected token {reader.TokenType} when parsing a smart flag enum.");
            }

            TEnum GetFromName(string name)
            {
                try
                {
                    return SmartFlagEnum<TEnum, TValue>.FromName(name, false).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new JsonException($"Error converting value '{name}' to a smart flag enum.", ex);
                }
            }
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
            else
                writer.WriteStringValue(value.Name.ToString());
        }
    }
}
