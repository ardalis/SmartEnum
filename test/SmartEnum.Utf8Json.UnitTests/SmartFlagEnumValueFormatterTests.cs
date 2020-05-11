using System;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Utf8Json;
using Utf8Json.Resolvers;
using Xunit;

namespace Ardalis.SmartEnum.Utf8Json.UnitTests
{
    public class SmartFlagEnumValueFormatterTests
    {
        public class TestClass
        {
            [JsonFormatter(typeof(SmartFlagEnumValueFormatter<FlagTestEnumInt16, short>))]
            public FlagTestEnumInt16 Int16 { get; set; }

            [JsonFormatter(typeof(SmartFlagEnumValueFormatter<FlagTestEnumInt32, int>))]
            public FlagTestEnumInt32 Int32 { get; set; }

            [JsonFormatter(typeof(SmartFlagEnumValueFormatter<FlagTestEnumDouble, double>))]
            public FlagTestEnumDouble Double { get; set; }
        }

        private static readonly TestClass TestInstance = new TestClass()
        {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance,
            Double = FlagTestEnumDouble.Instance
        };

        static readonly string JsonString = @"{""Int16"":1,""Int32"":1,""Double"":1}";

        static SmartFlagEnumValueFormatterTests()
        {
            CompositeResolver.Register(
                new SmartFlagEnumValueFormatter<FlagTestEnumInt16, short>(),
                new SmartFlagEnumValueFormatter<FlagTestEnumInt32, int>(),
                new SmartFlagEnumValueFormatter<FlagTestEnumDouble, double>()
            );
        }

        [Fact]
        public void SerializeValues()
        {
            var json = JsonSerializer.Serialize(TestInstance);

            Encoding.UTF8.GetString(json).Should().Be(JsonString);
        }

        [Fact]
        public void DeserializeValues()
        {
            var obj = JsonSerializer.Deserialize<TestClass>(JsonString);

            obj.Int16.Should().BeSameAs(FlagTestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(FlagTestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(FlagTestEnumDouble.Instance);
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
        public void DeserializesNull()
        {
            string json = @"{""Int16"": null, ""Int32"": null, ""Double"": null }";

            var obj = JsonSerializer.Deserialize<TestClass>(json);

            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{""Int32"": 3}";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnumInt32)} with Value 3 found.");
        }

        public static TheoryData<string, string> InvalidData =>
            new TheoryData<string, string>()
            {
                {@"{ ""Int16"": true }", @"expected:'Number Token', actual:'true', at offset:11"},
                {@"{ ""Int32"": true }", @"expected:'Number Token', actual:'true', at offset:11"},
                {@"{ ""Double"": true }", @"expected:'Number Token', actual:'true', at offset:12"},
            };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public void DeserializeThrowsWhenNotValid(string json, string message)
        {
            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonParsingException>()
                .WithMessage(message);
        }
    }
}