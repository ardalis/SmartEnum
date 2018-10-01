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
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
            }

            try
            {
                if (reader.TokenType == JsonToken.String && objectType.IsSmartEnum(out var underlyingType))
                {
                    var valueText = reader.Value.ToString();
                    if (valueText.Length == 0)
                    {
                        return null;
                    }

                    var converter = TypeDescriptor.GetConverter(underlyingType);
                    var value = converter.ConvertFromInvariantString(valueText);

                    var enumValue = objectType.GetMethod("FromValue", 
                        BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, 
                        new Type[] { underlyingType }, null)
                        .Invoke(null, new object[] { value });

                    return enumValue;
                }
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", ex);
            }

            // we don't actually expect to get here.
            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enum.");
        }       

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueType = value.GetType();
            var valueProperty = valueType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
            var valueValue = valueProperty.GetValue(value);
            var converter = TypeDescriptor.GetConverter(valueType);
            writer.WriteValue(converter.ConvertToInvariantString(valueValue));
        }
    }
}