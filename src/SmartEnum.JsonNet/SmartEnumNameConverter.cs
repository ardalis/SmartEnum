using Newtonsoft.Json;
using System;
using System.Reflection;

namespace SmartEnum.JsonNet
{
    public class SmartEnumNameConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType.IsSmartEnum();
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType)
            {
                case JsonToken.String:
                    return GetFromName(objectType, reader.Value.ToString());  
                            
                default:
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart enum.");
            }
        }       

        object GetFromName(Type objectType, string name)
        {
            try
            {
                return GeneratedMethods.FromName(objectType).Invoke(name);  
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting value '{name}' to a smart enum.", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var objectType = value.GetType();
            var objectName = GeneratedMethods.GetName(objectType).Invoke(value);
            writer.WriteValue(objectName);
        }
    }
}