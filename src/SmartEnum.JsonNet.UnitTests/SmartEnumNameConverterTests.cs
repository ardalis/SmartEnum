namespace Ardalis.SmartEnum.JsonNet.UnitTests
{
    using System;
    using Newtonsoft.Json;
    using Xunit;
    using FluentAssertions;

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
        }

        static readonly TestClass TestInstance = new TestClass { 
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
         };

        static readonly string JsonString =
@"{
  ""Bool"": ""Instance"",
  ""Int16"": ""Instance"",
  ""Int32"": ""Instance"",
  ""Double"": ""Instance""
}";

        [Fact]
        public void SerializesNames()
        {
            var json = JsonConvert.SerializeObject(TestInstance, Formatting.Indented);

            json.Should().Be(JsonString);
         }

        [Fact]
        public void DeserializesNames()
        {
            var obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

            obj.Bool.Should().BeSameAs(TestEnumBoolean.Instance);
            obj.Int16.Should().BeSameAs(TestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(TestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(TestEnumDouble.Instance);
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
        }    

        [Fact]
        public void DeserializeThrowsWhenNotFound()
        {
            string json = @"{ ""Bool"": ""Not Found"" }";

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Error converting value 'Not Found' to a smart enum.")
                .WithInnerException<SmartEnumNotFoundException>()
                .WithMessage($@"No {nameof(TestEnumBoolean)} with Name ""Not Found"" found.");
        }  


        [Fact]
        public void DeserializeThrowsWhenNull()
        {
            string json = @"{ ""Bool"": null }";

            Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

            act.Should()
                .Throw<JsonSerializationException>()
                .WithMessage($@"Unexpected token Null when parsing a smart enum.");
        }  

    }
}