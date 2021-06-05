namespace Ardalis.SmartEnum.SystemTextJson
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class SmartEnumNameConverter<TEnum, TValue> : JsonConverter<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IConvertible
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return GetFromName(reader.GetString());

                default:
                    throw new JsonException($"Unexpected token {reader.TokenType} when parsing a smart enum.");
            }
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Name.ToString());
        }

        private TEnum GetFromName(string name)
        {
            try
            {
                return SmartEnum<TEnum, TValue>.FromName(name, false);
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error converting value '{name}' to a smart enum.", ex);
            }
        }
    }
}