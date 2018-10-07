using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;

namespace Ardalis.SmartEnum.JsonNet
{
    public class SmartEnumValueConverter<TValue> : JsonConverter<ISmartEnum<TValue>>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ISmartEnum<TValue> ReadJson(JsonReader reader, Type objectType, ISmartEnum<TValue> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            try
            {
                var valueType = typeof(TValue);
                if (reader.TokenType == JsonToken.Integer && valueType != typeof(long) && valueType != typeof(bool))
                {
                    // explicit cast is required
                    value = Convert.ChangeType(value, valueType);
                }
                return (ISmartEnum<TValue>)GeneratedMethods.FromValue(objectType, value);
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting {value?.ToString() ?? "Null"} to {objectType.Name}.", ex);
            }
        }       

        public override void WriteJson(JsonWriter writer, ISmartEnum<TValue> value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Value);
        }
    }
}