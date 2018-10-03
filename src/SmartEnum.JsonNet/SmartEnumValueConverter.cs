using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace SmartEnum.JsonNet
{
    public class SmartEnumValueConverter : JsonConverter
    {
        static readonly ConcurrentDictionary<Type, Type> valueTypes = new ConcurrentDictionary<Type, Type>();
        
        public override bool CanConvert(Type objectType) => objectType.IsSmartEnum();
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var valueType = valueTypes.GetOrAdd(objectType, type => 
            { 
                if(type.IsSmartEnum(out var result))
                    return result; 

                return null;
            });

            var value = reader.Value;
            try
            {
                if (reader.TokenType == JsonToken.Integer && (
                    valueType == typeof(int) || valueType == typeof(uint) ||
                    valueType == typeof(byte) || valueType == typeof(char) ||
                    valueType == typeof(short) || valueType == typeof(ushort) ||
                    valueType == typeof(ulong) || valueType == typeof(float) ||
                    valueType == typeof(double)))
                {
                    // explicit cast is required
                    value = Convert.ChangeType(value, valueType);
                }
                return GeneratedMethods.FromValue(objectType, valueType).Invoke(value);
            }
            catch (Exception ex)
            {
                var valueString = value is null ? "Null" : value.ToString();
                throw new JsonSerializationException($"Error converting {valueString} to {objectType.Name}.", ex);
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