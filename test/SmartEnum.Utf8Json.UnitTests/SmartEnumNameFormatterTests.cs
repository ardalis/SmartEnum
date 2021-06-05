namespace Ardalis.SmartEnum.Utf8Json.UnitTests
{
    using System;
    using System.Text;
    using global::Utf8Json;
    using global::Utf8Json.Resolvers;
    using Xunit;
    using FluentAssertions;

    public class SmartEnumNameFormatterTests
    {
        public class TestClass
        {
            [JsonFormatter(typeof(SmartEnumNameFormatter<TestEnumBoolean, bool>))]
            public TestEnumBoolean Bool { get; set; }

            [JsonFormatter(typeof(SmartEnumNameFormatter<TestEnumInt16, short>))]
            public TestEnumInt16 Int16 { get; set; }

            [JsonFormatter(typeof(SmartEnumNameFormatter<TestEnumInt32, int>))]
            public TestEnumInt32 Int32 { get; set; }

            [JsonFormatter(typeof(SmartEnumNameFormatter<TestEnumDouble, double>))]
            public TestEnumDouble Double { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
        };

        static readonly string JsonString = @"{""Bool"":""Instance"",""Int16"":""Instance"",""Int32"":""Instance"",""Double"":""Instance""}";

        static SmartEnumNameFormatterTests()
        {
            CompositeResolver.Register(
                new SmartEnumNameFormatter<TestEnumBoolean, bool>(),
                new SmartEnumNameFormatter<TestEnumInt16, short>(),
                new SmartEnumNameFormatter<TestEnumInt32, int>(),
                new SmartEnumNameFormatter<TestEnumDouble, double>()
            );
        }

        [Fact]
        public void SerializesNames()
        {
            var json = JsonSerializer.Serialize(TestInstance);

            Encoding.UTF8.GetString(json).Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesNames()
        {
            var obj = JsonSerializer.Deserialize<TestClass>(JsonString);

            obj.Bool.Should().BeSameAs(TestEnumBoolean.Instance);
            obj.Int16.Should().BeSameAs(TestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(TestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(TestEnumDouble.Instance);
        }

        [Fact]
        public void DeserializesNullByDefault()
        {
            string json = @"{}";

            var obj = JsonSerializer.Deserialize<TestClass>(json);

            obj.Bool.Should().BeNull();
            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }


        [Fact]
        public void DeserializeNull()
        {
            string json = @"{ ""Bool"": null, ""Int16"": null, ""Int32"": null, ""Double"": null }";

            var obj = JsonSerializer.Deserialize<TestClass>(json);

            obj.Bool.Should().BeNull();
            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{ ""Bool"": ""Not Found"" }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnumBoolean)} with Name ""Not Found"" found.");
        }
    }
}