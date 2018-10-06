using System;
using SmartEnum.Exceptions;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;

namespace Ardalis.SmartEnum.JsonNet.UnitTests
{
    public class SmartEnumValueConverterTests
    {
        public class TestClass
        {
            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumBoolean Bool { get; set; }

            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumInt16 Int16 { get; set; }
            
            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumInt32 Int32 { get; set; }        

            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumDouble Double { get; set; }        

            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumString String { get; set; }        
        }

        public class TestIntClass
        {
            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumInt32 Property { get; set; }        
        }

        public class TestStringClass
        {
            [JsonConverter(typeof(SmartEnumValueConverter))]
            public TestEnumString Property { get; set; }        
        }

        static readonly TestClass TestInstance = new TestClass { 
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
  ""Double"": 1.0,
  ""String"": ""A string!""
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

            var obj = JsonConvert.DeserializeObject<TestClass>(json);

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

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting False to TestEnumBoolean.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnumBoolean)} with Value False found.");
        }  

        public static TheoryData<string, string> NotValidData =>
            new TheoryData<string, string> 
            {
                { @"{ ""Bool"": 1 }", @"Error converting 1 to TestEnumBoolean." },
                { @"{ ""Int16"": true }", @"Error converting True to TestEnumInt16." },
                { @"{ ""Int32"": true }", @"Error converting True to TestEnumInt32." },
                { @"{ ""Double"": true }", @"Error converting True to TestEnumDouble." },
                { @"{ ""String"": 1 }", @"Error converting 1 to TestEnumString." },
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
            string json = @"{ ""Bool"": null }";

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting Null to TestEnumBoolean.");
        }   
    }
}