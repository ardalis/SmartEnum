using Ardalis.SmartEnum;
using Ardalis.SmartEnum.JsonNet;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Ardalis.SmartEnum.JsonNet.UnitTests
{
    public class SmartFlagEnumValueConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumInt16, short>))]
            public FlagTestEnums.FlagTestEnumInt16 Int16 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumInt32, int>))]
            public FlagTestEnums.FlagTestEnumInt32 Int32 { get; set; }

            [JsonConverter(typeof(SmartFlagEnumValueConverter<FlagTestEnums.FlagTestEnumDouble, double>))]
            public FlagTestEnums.FlagTestEnumDouble Double { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass
        {
            Int16 = FlagTestEnums.FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnums.FlagTestEnumInt32.Instance2,
            Double = FlagTestEnums.FlagTestEnumDouble.Instance
        };

        static readonly string JsonString = JsonConvert.SerializeObject(new
        {
            Int16 = 1,
            Int32 = 2,
            Double = 1.0
        }, Formatting.Indented);
        
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

            Assert.Equal(obj.Int32, FlagTestEnums.FlagTestEnumInt32.Instance2);
            Assert.Equal(obj.Int16, FlagTestEnums.FlagTestEnumInt16.Instance);
            Assert.Equal(obj.Double, FlagTestEnums.FlagTestEnumDouble.Instance);
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

        [Fact]
        public void DeserializeThrowsWhenImplicitFlagEnumValueIsGiven()
        {
            string json = @"{ ""int32"": 3 }";

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            Assert.True(FlagTestEnums.FlagTestEnumInt32.Instance.Value == 1);
            Assert.True(FlagTestEnums.FlagTestEnumInt32.Instance2.Value == 2); 

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting 3 to FlagTestEnumInt32.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(FlagTestEnums.FlagTestEnumInt32)} with Value 3 found.");
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
