using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Ardalis.SmartEnum.JsonNet
{
    public class SmartEnumValueConverter : JsonConverter
    {
        static readonly ConcurrentDictionary<Type, Type> valueTypes = new ConcurrentDictionary<Type, Type>();

        public override bool CanConvert(Type objectType) => objectType.IsSmartEnum();
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var valueType = valueTypes.GetOrAdd(objectType, type => type.GetValueType());
            var value = reader.Value;
            try
            {
                if (reader.TokenType == JsonToken.Integer && valueType != typeof(long) && valueType != typeof(bool))
                {
                    // explicit cast is required
                    value = Convert.ChangeType(value, valueType);
                }
                return GeneratedMethods.FromValue(objectType, value);
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting {value?.ToString() ?? "Null"} to {objectType.Name}.", ex);
            }
        }       

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            GeneratedMethods.WriteValue(writer, value);
        }
    }
}