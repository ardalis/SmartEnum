using System.Collections.Generic;

namespace Ardalis.SmartEnum.JsonNet
{
    using Newtonsoft.Json;
    using System;

    public class SmartEnumNameConverter<TEnum, TValue> : JsonConverter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

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

        public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Name);
        }
    }
}