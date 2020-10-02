using System;
using System.Text;
using FluentAssertions;
using Utf8Json;
using Utf8Json.Resolvers;
using Xunit;

namespace Ardalis.SmartEnum.Utf8Json.UnitTests
{
    public class SmartFlagEnumNameFormatterTests
    {
        public class TestClass
        {
            [JsonFormatter(typeof(SmartFlagEnumNameFormatter<FlagTestEnumInt16, short>))]
            public FlagTestEnumInt16 Int16 { get; set; }

            [JsonFormatter(typeof(SmartFlagEnumNameFormatter<FlagTestEnumInt32, int>))]
            public FlagTestEnumInt32 Int32 { get; set; }

            [JsonFormatter(typeof(SmartFlagEnumNameFormatter<FlagTestEnumDouble, double>))]
            public FlagTestEnumDouble Double { get; set; }
        }

        private static readonly TestClass TestInstance = new TestClass()
        {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance2,
            Double = FlagTestEnumDouble.Instance
        };

        static readonly string JsonString = @"{""Int16"":""Instance"",""Int32"":""Instance2"",""Double"":""Instance""}";

        static SmartFlagEnumNameFormatterTests()
        {
            CompositeResolver.Register(
                new SmartFlagEnumNameFormatter<FlagTestEnumInt16, short>(),
                new SmartFlagEnumNameFormatter<FlagTestEnumInt32, int>(),
                new SmartFlagEnumNameFormatter<FlagTestEnumDouble, double>()
            );
        }

        [Fact]
        public void SerializeNames()
        {
            var json = JsonSerializer.Serialize(TestInstance);

            Encoding.UTF8.GetString(json).Should().Be(JsonString);
        }

        [Fact]
        public void DeserializeNames()
        {
            var obj = JsonSerializer.Deserialize<TestClass>(JsonString);

            obj.Int16.Should().BeSameAs(FlagTestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(FlagTestEnumInt32.Instance2);
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
        public void DeserializeNull()
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
            string json = @"{""Int32"": ""Not Found""}";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnumInt32)} with Name ""Not Found"" found.");
        }
    }
}