namespace Ardalis.SmartEnum.SystemTextJson.UnitTests
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Xunit;

    public class SmartEnumNameConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartEnumNameConverter<TestEnumBoolean, bool>))]
            public TestEnumBoolean Bool { get; set; }

            [JsonConverter(typeof(SmartEnumNameConverter<TestEnumInt16, short>))]
            public TestEnumInt16 Int16 { get; set; }

            [JsonConverter(typeof(SmartEnumNameConverter<TestEnumInt32, int>))]
            public TestEnumInt32 Int32 { get; set; }

            [JsonConverter(typeof(SmartEnumNameConverter<TestEnumDouble, double>))]
            public TestEnumDouble Double { get; set; }

            [JsonConverter(typeof(SmartEnumNameConverter<TestEnumString, string>))]
            public TestEnumString String { get; set; }

            public IDictionary<TestEnumInt32, string> DictInt32String { get; set; }

            public IDictionary<TestEnumString, string> DictStringString { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
            String = TestEnumString.Instance,
            DictInt32String = TestDictInt32EnumString.Instance,
            DictStringString = TestDictStringEnumString.Instance
        };

        static readonly string JsonString = JsonSerializer.Serialize(new
        {
            Bool = "Instance",
            Int16 = "Instance",
            Int32 = "Instance",
            Double = "Instance",
            String = "Instance",
            DictInt32String = new { Instance = "Instance" },
            DictStringString = new { Instance = "Instance" }
        }, TestJsonConverters.NameConverterOptions);

        [Fact]
        public void SerializesNames()
        {
            var json = JsonSerializer.Serialize(TestInstance, TestJsonConverters.NameConverterOptions);

            json.Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesNames()
        {
            var obj = JsonSerializer.Deserialize<TestClass>(JsonString, TestJsonConverters.NameConverterOptions);

            obj.Bool.Should().BeSameAs(TestEnumBoolean.Instance);
            obj.Int16.Should().BeSameAs(TestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(TestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(TestEnumDouble.Instance);
            obj.String.Should().BeSameAs(TestEnumString.Instance);
            obj.DictInt32String.Should().BeEquivalentTo(TestDictInt32EnumString.Instance);
            obj.DictStringString.Should().BeEquivalentTo(TestDictStringEnumString.Instance);
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
            obj.String.Should().BeNull();
            obj.DictInt32String.Should().BeNull();
            obj.DictStringString.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{ ""Bool"": ""Not Found"" }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Error converting value 'Not Found' to a smart enum.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnumBoolean)} with Name ""Not Found"" found.");
        }


        // JsonSerializer doesn't call the converter on null values
        //[Fact]
        //public void DeserializeThrowsWhenNull()
        //{
        //    string json = @"{ ""Bool"": null }";

        //    Action act = () => JsonSerializer.Deserialize<TestClass>(json);

        //    act.Should()
        //        .Throw<JsonException>()
        //        .WithMessage($@"Unexpected token Null when parsing a smart enum.");
        //}
    }
}