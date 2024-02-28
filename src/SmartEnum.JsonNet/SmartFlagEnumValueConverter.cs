using System;
using Newtonsoft.Json;

namespace Ardalis.SmartEnum.JsonNet;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEnum"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class SmartFlagEnumValueConverter<TEnum, TValue> : JsonConverter<TEnum>
where TEnum : SmartFlagEnum<TEnum, TValue>
where TValue: struct, IEquatable<TValue>, IComparable<TValue>
{
    /// <summary>
    /// Defaults to true.
    /// </summary>
    public override bool CanRead => true;

    /// <summary>
    /// Defaults to true.
    /// </summary>
    public override bool CanWrite => true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    public override TEnum ReadJson(JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        try
        {
            TValue value;
            if (reader.TokenType == JsonToken.Integer && typeof(TValue) != typeof(long) && typeof(TValue) != typeof(bool))
            {
                value = (TValue)Convert.ChangeType(reader.Value, typeof(TValue));
            }
            else
            {
                value = (TValue)reader.Value;
            }

            return SmartFlagEnum<TEnum, TValue>.DeserializeValue(value);
        }
        catch (Exception ex)
        {
            throw new JsonSerializationException($"Error converting {reader.Value ?? "Null"} to {objectType.Name}.", ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
    {
        if (value is null)
            writer.WriteNull();
        else
            writer.WriteValue(value.Value);
    }

}
