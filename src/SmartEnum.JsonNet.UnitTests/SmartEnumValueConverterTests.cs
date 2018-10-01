using System;
using SmartEnum;
using SmartEnum.JsonNet;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;

namespace SmartEnum.JsonNet.UnitTests
{
    public class SmartEnumValueConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnum SerializeValue { get; set; }
        }

        static readonly TestClass TestInstance = new TestClass { 
            SerializeValue = TestEnum.Three,
         };

        static readonly string JsonString = @"{""SerializeValue"":""3""}";

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
    }
}