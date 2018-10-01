using System;
using SmartEnum;
using SmartEnum.Exceptions;
using SmartEnum.JsonNet;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;

namespace SmartEnum.JsonNet.UnitTests
{
    public class SmartEnumNameConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartEnumNameConverter))]
            public TestEnum SerializeValue { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass { 
            SerializeValue = TestEnum.Three,
         };

        static readonly string JsonString = @"{""SerializeValue"":""Three""}";

        [Fact]
        public void SerializesValue()
        {
            var json = JsonConvert.SerializeObject(TestInstance);

            json.Should().Be(JsonString);
         }

        [Fact]
        public void DeserializesValue()
        {
            var obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

            obj.SerializeValue.Should().BeSameAs(TestInstance.SerializeValue);
        }    

        [Fact]
        public void DeserializeThrowsIfNotValid()
        {
            var invalidValue = "Invalid";
            Action act = () => JsonConvert.DeserializeObject<TestClass>($@"{{""SerializeValue"":""{invalidValue}""}}");
            
            act.Should()
                .Throw<JsonSerializationException>()
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnum)} with Name ""{invalidValue}"" found.");
        }      
    }
}