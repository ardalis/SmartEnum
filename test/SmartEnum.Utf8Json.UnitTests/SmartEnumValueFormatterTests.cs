namespace Ardalis.SmartEnum.Utf8Json.UnitTests
{
    using System;
    using System.Text;
    using global::Utf8Json;
    using global::Utf8Json.Resolvers;
    using Xunit;
    using FluentAssertions;

    public class SmartEnumValueFormatterTests
    {
        public class TestClass
        {
            [JsonFormatter(typeof(SmartEnumValueFormatter<TestEnumBoolean, bool>))]
            public TestEnumBoolean Bool { get; set; }

            [JsonFormatter(typeof(SmartEnumValueFormatter<TestEnumInt16, short>))]
            public TestEnumInt16 Int16 { get; set; }

            [JsonFormatter(typeof(SmartEnumValueFormatter<TestEnumInt32, int>))]
            public TestEnumInt32 Int32 { get; set; }

            [JsonFormatter(typeof(SmartEnumValueFormatter<TestEnumDouble, double>))]
            public TestEnumDouble Double { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
        };

        static readonly string JsonString = @"{""Bool"":true,""Int16"":1,""Int32"":1,""Double"":1}";

        static SmartEnumValueFormatterTests()
        {
            CompositeResolver.Register(
                new SmartEnumValueFormatter<TestEnumBoolean, bool>(),
                new SmartEnumValueFormatter<TestEnumInt16, short>(),
                new SmartEnumValueFormatter<TestEnumInt32, int>(),
                new SmartEnumValueFormatter<TestEnumDouble, double>()
            );
        }

        [Fact]
        public void SerializesValue()
        {
            var json = JsonSerializer.Serialize(TestInstance);

            Encoding.UTF8.GetString(json).Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesValue()
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
        public void DeserializesNull()
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
            string json = @"{ ""Bool"": false }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnumBoolean)} with Value False found.");
        }

        public static TheoryData<string, string> NotValidData =>
            new TheoryData<string, string>
            {
                { @"{ ""Bool"": 1 }", @"expected:'true | false', actual:'1', at offset:10" },
                { @"{ ""Int16"": true }", @"expected:'Number Token', actual:'true', at offset:11" },
                { @"{ ""Int32"": true }", @"expected:'Number Token', actual:'true', at offset:11" },
                { @"{ ""Double"": true }", @"expected:'Number Token', actual:'true', at offset:12" },
            };

        [Theory]
        [MemberData(nameof(NotValidData))]
        public void DeserializeThrowsWhenNotValid(string json, string message)
        {
            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonParsingException>()
                .WithMessage(message);
        }
    }
}