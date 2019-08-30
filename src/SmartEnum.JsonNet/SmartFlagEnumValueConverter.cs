using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Ardalis.SmartEnum.JsonNet
{
    public class SmartFlagEnumValueConverter<Tenum, Tvalue> : JsonConverter<IEnumerable<Tenum>>
    where Tenum : SmartFlagEnum<Tenum, Tvalue>
    where Tvalue: struct, IEquatable<Tvalue>, IComparable<Tvalue>
    {
        public override bool CanRead { get; } = true;
        public override bool CanWrite { get; } = true;

        public override IEnumerable<Tenum> ReadJson(JsonReader reader, Type objectType, IEnumerable<Tenum> existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            try
            {
                Tvalue value;
                if (reader.TokenType == JsonToken.Integer && typeof(Tvalue) != typeof(long) &&
                    typeof(Tvalue) != typeof(bool))
                {
                    value = (Tvalue) Convert.ChangeType(reader.Value, typeof(Tvalue));
                }
                else
                {
                    value = (Tvalue) reader.Value;
                }

                return SmartFlagEnum<Tenum, Tvalue>.FromValue(value);
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting {reader.Value ?? "Null"} to {objectType.GetGenericArguments().FirstOrDefault().Name}.", ex);
            }
        }
        public override void WriteJson(JsonWriter writer, IEnumerable<Tenum> value, JsonSerializer serializer)
        {
            var outputValue = 0;
            if (value is null)
                writer.WriteNull();
            else
            {
                var enumList = value.ToList();
                foreach (var smartFlagEnum in enumList)
                {
                    outputValue += int.Parse(smartFlagEnum.Value.ToString());
                }
            }

            var output = Convert.ChangeType(outputValue, typeof(Tvalue));
            writer.WriteValue(output);
        }

    }
}
