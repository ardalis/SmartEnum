using Newtonsoft.Json;
using System;

namespace Ardalis.SmartEnum.JsonNet
{
    public class SmartEnumNameConverter<TValue> : JsonConverter<ISmartEnum<TValue>>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ISmartEnum<TValue> ReadJson(JsonReader reader, Type objectType, ISmartEnum<TValue> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType)
            {
                case JsonToken.String:
                    return GetFromName(objectType, (string)reader.Value);  
                            
                default:
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart enum.");
            }
        }       

        ISmartEnum<TValue> GetFromName(Type objectType, string name)
        {
            try
            {
                return (ISmartEnum<TValue>)GeneratedMethods.FromName(objectType, name);  
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting value '{name}' to a smart enum.", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, ISmartEnum<TValue> value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Name);
        }
    }
}