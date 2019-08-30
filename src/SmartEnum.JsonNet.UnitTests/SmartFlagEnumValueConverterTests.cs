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
    public class SmartFlagEnumValueConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumInt16, short>))]
            public IEnumerable<FlagTestEnums.FlagTestEnumInt16> Int16 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumInt32, int>))]
            public IEnumerable<FlagTestEnums.FlagTestEnumInt32> Int32 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumDouble, double>))]
            public IEnumerable<FlagTestEnums.FlagTestEnumDouble> Double { get; set; }
        }

        public class TestIntClass
        {
            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumInt32, int>))]
            public TestEnumInt32 Property { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Int16 = new List<FlagTestEnums.FlagTestEnumInt16>() { FlagTestEnums.FlagTestEnumInt16.One, FlagTestEnums.FlagTestEnumInt16.Two },
            Int32 = new List<FlagTestEnums.FlagTestEnumInt32>() { FlagTestEnums.FlagTestEnumInt32.One, FlagTestEnums.FlagTestEnumInt32.Two, FlagTestEnums.FlagTestEnumInt32.Three, FlagTestEnums.FlagTestEnumInt32.Four },
            Double = new List<FlagTestEnums.FlagTestEnumDouble>() { FlagTestEnums.FlagTestEnumDouble.One }
        };

        static readonly string JsonString = 
            @"{
  ""Int16"": 3,
  ""Int32"": 15,
  ""Double"": 1.0
}";
        [Fact]
        public void SerializesValue()
        {
            var json = JsonConvert.SerializeObject(TestInstance, Formatting.Indented);

            json.Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesValue()
        {
            var obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

            Assert.Equal(obj.Int32, TestInstance.Int32);
            Assert.Equal(obj.Int16, TestInstance.Int16);
            Assert.Equal(obj.Double, TestInstance.Double);
        }

        [Fact]
        public void DeserializeValueAlternative()
        {
            const string json = @"{""int32"": 3 }";

            var obj = JsonConvert.DeserializeObject<TestClass>(json);
            var list = obj.Int32.ToList();

            Assert.Equal(2, obj.Int32.Count());
            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].Name);
        }

        [Fact]
        public void DeserializesNullByDefault()
        {
            string json = @"{}";

            var obj = JsonConvert.DeserializeObject<TestClass>(json);

            obj.Int16.Should().BeNull();
            obj.Int32.Should().BeNull();
            obj.Double.Should().BeNull();
        }

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{ ""int32"": 42 }";

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting 42 to FlagTestEnumInt32.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnums.FlagTestEnumInt32)} with Value 42 found.");
        }

        public static TheoryData<string, string> NotValidData =>
            new TheoryData<string, string>
            {
                { @"{ ""Int16"": true }", @"Error converting True to FlagTestEnumInt16." },
                { @"{ ""Int32"": true }", @"Error converting True to FlagTestEnumInt32." },
                { @"{ ""Double"": true }", @"Error converting True to FlagTestEnumDouble." },
            };

        [Theory]
        [MemberData(nameof(NotValidData))]
        public void DeserializeThrowsWhenNotValid(string json, string message)
        {
            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage(message);
        }

        [Fact]
        public void DeserializeThrowsWhenNull()
        {
            string json = @"{ ""int32"": null }";

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting Null to FlagTestEnumInt32.");
        }
    }
}
