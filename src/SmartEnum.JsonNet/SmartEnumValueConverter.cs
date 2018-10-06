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
                if (reader.TokenType == JsonToken.Integer && valueType != typeof(long) && IsNumeric(valueType))
                {
                    // explicit cast is required
                    value = Convert.ChangeType(value, valueType);
                }
                return GeneratedMethods.FromValue(objectType, valueType).Invoke(value);
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

            var smartEnum = (ISmartEnum)value;
            writer.WriteValue(smartEnum.Value);
        }

        // Source: https://stackoverflow.com/a/13179018/861773
        public static bool IsNumeric(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumeric(Nullable.GetUnderlyingType(type));
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}