using System.Collections.Generic;

namespace Ardalis.SmartEnum.JsonNet
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmartEnumNameConverter<TEnum, TValue> : JsonConverter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        /// <summary>
        /// Defaults to true.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Defaults to true.
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="hasExistingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="JsonSerializationException"></exception>
        public override TEnum ReadJson(JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    return GetFromName((string)reader.Value);

                default:
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart enum.");
            }

            TEnum GetFromName(string name)
            {
                try
                {
                    return SmartEnum<TEnum, TValue>.FromName(name, false);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException($"Error converting value '{name}' to a smart enum.", ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Name);
        }
    }
}