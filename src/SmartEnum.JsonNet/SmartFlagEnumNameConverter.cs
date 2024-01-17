using System;
using System.Linq;
using Newtonsoft.Json;

namespace Ardalis.SmartEnum.JsonNet
{
    public class SmartFlagEnumNameConverter<TEnum, TValue> : JsonConverter<TEnum> 
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : struct, IComparable<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Default to true.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Defaults to true
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
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart flag enum.");
            }

            TEnum GetFromName(string name)
            {
                try
                {
                    return SmartFlagEnum<TEnum, TValue>.FromName(name, false).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException($"Error converting value '{name}' to a smart flag enum.", ex);
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
