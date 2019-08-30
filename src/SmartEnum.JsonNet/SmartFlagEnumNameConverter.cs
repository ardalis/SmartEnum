using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Ardalis.SmartEnum.JsonNet
{
    public class SmartFlagEnumNameConverter<TEnum, TValue> : JsonConverter<IEnumerable<TEnum>> 
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : struct, IComparable<TValue>, IEquatable<TValue>
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override IEnumerable<TEnum> ReadJson(JsonReader reader, Type objectType, IEnumerable<TEnum> existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    return GetFromName((string)reader.Value);

                default:
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart flag enum.");
            }

            IEnumerable<TEnum> GetFromName(string name)
            {
                try
                {
                    return SmartFlagEnum<TEnum, TValue>.FromName(name, false);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException($"Error converting value '{name}' to a list of smart flag enums.", ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, IEnumerable<TEnum> value, JsonSerializer serializer)
        {
            var sb = new StringBuilder();
            if (value is null)
                writer.WriteNull();
            else
            {
                var enumList = value.ToList();
                foreach (var smartFlagEnum in enumList)
                {
                    sb.Append(smartFlagEnum.Name);
                    if (enumList.Last().Name != smartFlagEnum.Name && enumList.Count > 1)
                    {
                        sb.Append(", ");
                    }
                }
            }

            writer.WriteValue(sb.ToString());
        }
    }
}
