namespace Ardalis.SmartEnum.SystemTextJson.UnitTests
{
    using FluentAssertions;
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Xunit;

    public class SmartFlagEnumNameConverterTests
    {
        public class FlagTestClass
        {
            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnumInt16, short>))]
            public FlagTestEnumInt16 Int16 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnumInt32, int>))]
            public FlagTestEnumInt32 Int32 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumNameConverter<FlagTestEnumDouble, double>))]
            public FlagTestEnumDouble Double { get; set; }
        }

        static readonly FlagTestClass TestInstance = new FlagTestClass {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance,
            Double = FlagTestEnumDouble.Instance
        };

        static readonly string JsonString = JsonSerializer.Serialize(new
        {
            Int16 = "Instance",
            Int32 = "Instance",
            Double = "Instance"
        }, new JsonSerializerOptions { WriteIndented = true });

        [Fact]
        public void SerializesNames()
        {
            var json = JsonSerializer.Serialize(TestInstance, new JsonSerializerOptions { WriteIndented = true });

            json.Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesNames()
        {
            var obj = JsonSerializer.Deserialize<FlagTestClass>(JsonString);

            obj.Int16.Should().BeSameAs(FlagTestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(FlagTestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(FlagTestEnumDouble.Instance);
        }

        [Fact]
        public void DeserializesNullByDefault()
        {
            const string json = @"{}";

            var obj = JsonSerializer.Deserialize<FlagTestClass>(json);

            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            const string json = @"{ ""Int32"": ""Not Found"" }";

            Action act = () => JsonSerializer.Deserialize<FlagTestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Error converting value 'Not Found' to a smart flag enum.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnumInt32)} with Name ""Not Found"" found.");
        }

        [Fact]
        public void DeserializeThrowsWhenNull()
        {
            const string json = @"{ ""Int16"": null }";

            Action act = () => JsonSerializer.Deserialize<FlagTestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Unexpected token Null when parsing a smart flag enum.");
        }
    }
}