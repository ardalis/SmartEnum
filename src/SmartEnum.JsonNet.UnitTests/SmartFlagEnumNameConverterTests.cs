using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.JsonNet;
using Ardalis.SmartEnum.JsonNet.UnitTests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace SmartEnum.JsonNet.UnitTests
{
    public class SmartFlagEnumNameConverterTests
    {
        public class FlagTestClass
        {
            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnums.FlagTestEnumInt16, short>))]
            public IEnumerable<FlagTestEnums.FlagTestEnumInt16> Int16 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnums.FlagTestEnumInt32, int>))]
            public IEnumerable<FlagTestEnums.FlagTestEnumInt32> Int32 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnums.FlagTestEnumDouble, double>))]
            public IEnumerable<FlagTestEnums.FlagTestEnumDouble> Double { get; set; }
        }

        static readonly FlagTestClass TestInstance = new FlagTestClass
        {
            Int16 = new List<FlagTestEnums.FlagTestEnumInt16>() { FlagTestEnums.FlagTestEnumInt16.One, FlagTestEnums.FlagTestEnumInt16.Two },
            Int32 = new List<FlagTestEnums.FlagTestEnumInt32>() { FlagTestEnums.FlagTestEnumInt32.One},
            Double = new List<FlagTestEnums.FlagTestEnumDouble>() { FlagTestEnums.FlagTestEnumDouble.One}
        };

        private const string JsonString = @"{
  ""Int16"": ""One, Two"",
  ""Int32"": ""One"",
  ""Double"": ""One""
}";

        [Fact]
        public void SerializesNames()
        {
            var json = JsonConvert.SerializeObject(TestInstance, Formatting.Indented);

            json.Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesNames()
        {
            var obj = JsonConvert.DeserializeObject<FlagTestClass>(JsonString);

            Assert.Equal(obj.Int32, TestInstance.Int32);
            Assert.Equal(obj.Int16, TestInstance.Int16);
            Assert.Equal(obj.Double, TestInstance.Double);
        }

        [Fact]
        public void DeserializeMultipleNamesInt16()
        {
            const string json = @"{""int16"": ""One, Two, Three, Four""}";

            var obj = JsonConvert.DeserializeObject<FlagTestClass>(json);
            var list = obj.Int16.ToList();

            Assert.Equal(3, obj.Int16.Count());
            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].Name);
            Assert.Equal("Three", list[2].Name);
        }

        [Fact]
        public void DeserializeMultipleNamesInt32()
        {
            const string json = @"{""int32"": ""One, Two, Three""}";

            var obj = JsonConvert.DeserializeObject<FlagTestClass>(json);
            var list = obj.Int32.ToList();

            Assert.Equal(3, obj.Int32.Count());
            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].Name);
            Assert.Equal("Three", list[2].Name);
        }

        [Fact]
        public void DeserializeMultipleNamesDouble()
        {
            const string json = @"{""double"": ""One, Two, Three, Four""}";

            var obj = JsonConvert.DeserializeObject<FlagTestClass>(json);
            var list = obj.Double.ToList();

            Assert.Equal(3, obj.Double.Count());
            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].Name);
            Assert.Equal("Three", list[2].Name);
        }

        [Fact]
        public void DeserializesNullByDefault()
        {
            const string json = @"{}";

            var obj = JsonConvert.DeserializeObject<FlagTestClass>(json);

            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNamesAreSpelledWrong()
        {
            const string json = @"{""int16"": ""Oneses, Twtto, Threee, Foursiesz""}";

            Action act = () => JsonConvert.DeserializeObject<FlagTestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting value 'Oneses, Twtto, Threee, Foursiesz' to a list of smart flag enums.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnums.FlagTestEnumInt16)} with Name ""Oneses, Twtto, Threee, Foursiesz"" found.");
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            const string json = @"{ ""int32"": ""Not Found"" }";

            Action act = () => JsonConvert.DeserializeObject<FlagTestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting value 'Not Found' to a list of smart flag enums.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnums.FlagTestEnumInt32)} with Name ""Not Found"" found.");
        }

        [Fact]
        public void DeserializeThrowsWhenNull()
        {
            const string json = @"{ ""int16"": null }";

            Action act = () => JsonConvert.DeserializeObject<FlagTestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Unexpected token Null when parsing a smart flag enum.");
        }
    }
}
