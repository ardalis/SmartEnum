using Ardalis.SmartEnum;
using FluentAssertions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Ardalis.SmartEnum.SystemTextJson.UnitTests
{
    public class SmartFlagEnumValueConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnumInt16, short>))]
            public FlagTestEnumInt16 Int16 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnumInt32, int>))]
            public FlagTestEnumInt32 Int32 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnumDouble, double>))]
            public FlagTestEnumDouble Double { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance2,
            Double = FlagTestEnumDouble.Instance
        };

        static readonly string JsonString = JsonSerializer.Serialize(new
        {
            Int16 = 1,
            Int32 = 2,
            Double = 1
        }, new JsonSerializerOptions { WriteIndented = true });
        
        [Fact]
        public void SerializesValue()
        {
            var json = JsonSerializer.Serialize(TestInstance, new JsonSerializerOptions { WriteIndented = true });

            json.Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesValue()
        {
            var obj = JsonSerializer.Deserialize<TestClass>(JsonString);

            Assert.Equal(obj.Int32, FlagTestEnumInt32.Instance2);
            Assert.Equal(obj.Int16, FlagTestEnumInt16.Instance);
            Assert.Equal(obj.Double, FlagTestEnumDouble.Instance);
        }

        [Fact]
        public void DeserializesNullByDefault()
        {
            string json = @"{}";

            var obj = JsonSerializer.Deserialize<TestClass>(json);

            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{ ""Int32"": 42 }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Error converting value '42' to a smart flag enum.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnumInt32)} with Value 42 found.");
        }

        [Fact]
        public void DeserializeThrowsWhenImplicitFlagEnumValueIsGiven()
        {
            string json = @"{ ""Int32"": 3 }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            Assert.True(FlagTestEnumInt32.Instance.Value == 1);
            Assert.True(FlagTestEnumInt32.Instance2.Value == 2); 

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Error converting value '3' to a smart flag enum.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnumInt32)} with Value 3 found.");
        }

        public static TheoryData<string, string> NotValidData =>
            new TheoryData<string, string>
            {
                { @"{ ""Int16"": true }", @"Error converting token 'True' to a smart flag enum." },
                { @"{ ""Int32"": true }", @"Error converting token 'True' to a smart flag enum." },
                { @"{ ""Double"": true }", @"Error converting token 'True' to a smart flag enum." },
            };

        [Theory]
        [MemberData(nameof(NotValidData))]
        public void DeserializeThrowsWhenNotValid(string json, string message)
        {
            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage(message);
        }

        [Fact]
        public void DeserializeThrowsWhenNull()
        {
            string json = @"{ ""Int32"": null }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Error converting value 'Null' to a smart flag enum.");
        }
    }
}
