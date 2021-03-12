namespace Ardalis.SmartEnum.SystemTextJson.UnitTests
{
    using FluentAssertions;
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Xunit;

    public class SmartEnumValueConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartEnumValueConverter<TestEnumBoolean, bool>))]
            public TestEnumBoolean Bool { get; set; }

            [JsonConverter(typeof(SmartEnumValueConverter<TestEnumInt16, short>))]
            public TestEnumInt16 Int16 { get; set; }

            [JsonConverter(typeof(SmartEnumValueConverter<TestEnumInt32, int>))]
            public TestEnumInt32 Int32 { get; set; }

            [JsonConverter(typeof(SmartEnumValueConverter<TestEnumDouble, double>))]
            public TestEnumDouble Double { get; set; }

            [JsonConverter(typeof(SmartEnumValueConverter<TestEnumString, string>))]
            public TestEnumString String { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
            String = TestEnumString.Instance,
        };

        static readonly string JsonString =
@"{
  ""Bool"": true,
  ""Int16"": 1,
  ""Int32"": 1,
  ""Double"": 1.2,
  ""String"": ""1.5""
}";

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

            obj.Bool.Should().BeSameAs(TestEnumBoolean.Instance);
            obj.Int16.Should().BeSameAs(TestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(TestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(TestEnumDouble.Instance);
            obj.String.Should().BeSameAs(TestEnumString.Instance);
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
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{ ""Bool"": false }";

            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithMessage($@"Error converting value 'False' to a smart enum.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnumBoolean)} with Value False found.");
        }

        public static TheoryData<string, string> NotValidData =>
            new TheoryData<string, string>
            {
                { @"{ ""Bool"": 1 }", @"Cannot get the value of a token type 'Number' as a boolean." },
                { @"{ ""Int16"": true }", @"Cannot get the value of a token type 'True' as a number." },
                { @"{ ""Int32"": true }", @"Cannot get the value of a token type 'True' as a number." },
                { @"{ ""Double"": true }", @"Cannot get the value of a token type 'True' as a number." },
                { @"{ ""String"": true }", @"Cannot get the value of a token type 'True' as a string." },
            };

        [Theory]
        [MemberData(nameof(NotValidData))]
        public void DeserializeThrowsWhenNotValid(string json, string message)
        {
            Action act = () => JsonSerializer.Deserialize<TestClass>(json);

            act.Should()
                .Throw<JsonException>()
                .WithInnerException<InvalidOperationException>()
                .WithMessage(message);
        }

        // JsonSerializer doesn't call the converter on null values
        //[Fact]
        //public void DeserializeThrowsWhenNull()
        //{
        //    string json = @"{ ""Bool"": null }";

        //    Action act = () => JsonSerializer.Deserialize<TestClass>(json);

        //    act.Should()
        //        .Throw<JsonException>()
        //        .WithMessage($@"Error converting Null to TestEnumBoolean.");
        //}   
    }
}