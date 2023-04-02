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

namespace Ardalis.SmartEnum.JsonNet.UnitTests
{
    public class SmartFlagEnumNameConverterTests
    {
        public class FlagTestClass
        {
            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnums.FlagTestEnumInt16, short>))]
            public FlagTestEnums.FlagTestEnumInt16 Int16 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnums.FlagTestEnumInt32, int>))]
            public FlagTestEnums.FlagTestEnumInt32 Int32 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnums.FlagTestEnumDouble, double>))]
            public FlagTestEnums.FlagTestEnumDouble Double { get; set; }
        }

        static readonly FlagTestClass TestInstance = new FlagTestClass
        {
            Int16 = FlagTestEnums.FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnums.FlagTestEnumInt32.Instance,
            Double = FlagTestEnums.FlagTestEnumDouble.Instance
        };

        static readonly string JsonString = JsonConvert.SerializeObject(new
        {
            Int16 = "Instance",
            Int32 = "Instance",
            Double = "Instance"
        }, Formatting.Indented);

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

            obj.Int16.Should().BeSameAs(FlagTestEnums.FlagTestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(FlagTestEnums.FlagTestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(FlagTestEnums.FlagTestEnumDouble.Instance);
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
        public void DeserializeThrowsWhenNotFound()
        {
            const string json = @"{ ""int32"": ""Not Found"" }";

            Action act = () => JsonConvert.DeserializeObject<FlagTestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting value 'Not Found' to a smart flag enum.")
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
