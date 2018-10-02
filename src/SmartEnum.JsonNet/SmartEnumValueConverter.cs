using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace SmartEnum.JsonNet
{
    public class SmartEnumValueConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType) => objectType.IsSmartEnum();
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            objectType.IsSmartEnum(out var valueType);

            if (reader.TokenType == JsonToken.Null && valueType.IsValueType)
                throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart enum.");

            var value = reader.Value;
            switch(Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    if(reader.TokenType != JsonToken.Boolean)
                        throw new JsonSerializationException($"'{reader.Value}' is not a boolean.");
                    break;

                case TypeCode.Int64:
                    if(reader.TokenType != JsonToken.Integer)
                        throw new JsonSerializationException($"'{reader.Value}' is not an integer.");
                    break;

                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    if(reader.TokenType != JsonToken.Integer)
                        throw new JsonSerializationException($"'{reader.Value}' is not an integer.");

                    // must explicitly convert from Int64 to value type
                    var converter = TypeDescriptor.GetConverter(valueType);
                    value = converter.ConvertTo(value, valueType);
                    break;

                case TypeCode.Single:
                case TypeCode.Double:
                    if(reader.TokenType != JsonToken.Float)
                        throw new JsonSerializationException($"'{reader.Value}' is not a float.");
                    break;

                case TypeCode.String:
                    if(reader.TokenType != JsonToken.String)
                        throw new JsonSerializationException($"'{reader.Value}' is not a string.");
                    break;

                default:
                    throw new JsonSerializationException($"Unexpected token '{reader.TokenType}' when parsing a smart enum.");
            }
       
            try
            {
                return GeneratedMethods.FromValue(objectType, valueType).Invoke(value);
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting value '{reader.Value}' to a smart enum.", ex);
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
            var objectValue = GeneratedMethods.GetValue(objectType).Invoke(value);
            writer.WriteValue(objectValue);
        }
    }
}