namespace Ardalis.SmartEnum.ProtoBufNet.UnitTests
{
    using System;
    using ProtoBuf;
    using ProtoBuf.Meta;
    using Xunit;
    using FluentAssertions;

    public class SmartEnumNameSurrogateTests
    {
        [ProtoContract]
        public class TestClass
        {
            [ProtoMember(1)]
            public TestEnumBoolean Bool { get; set; }

            [ProtoMember(2)]
            public TestEnumInt16 Int16 { get; set; }
            
            [ProtoMember(3)]
            public TestEnumInt32 Int32 { get; set; }        

            [ProtoMember(4)]
            public TestEnumDouble Double { get; set; }        
        }

        static readonly TestClass NullTestInstance = new TestClass { 
            Bool = null,
            Int16 = null,
            Int32 = null,
            Double = null,
        };

        static readonly TestClass TestInstance = new TestClass { 
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
        };

        readonly string SchemaString =
@"syntax = ""proto2"";
package Ardalis.SmartEnum.ProtoBufNet;

message SmartEnumNameSurrogate_TestEnumBoolean_Boolean {
   required string Name = 1;
}
";

        readonly RuntimeTypeModel model;

        public SmartEnumNameSurrogateTests()
        {
            model = RuntimeTypeModel.Create();
            model.Add(typeof(TestEnumBoolean), false).SetSurrogate(typeof(SmartEnumNameSurrogate<TestEnumBoolean, bool>));
            model.Add(typeof(TestEnumInt16), false).SetSurrogate(typeof(SmartEnumNameSurrogate<TestEnumInt16, short>));
            model.Add(typeof(TestEnumInt32), false).SetSurrogate(typeof(SmartEnumNameSurrogate<TestEnumInt32, int>));
            model.Add(typeof(TestEnumDouble), false).SetSurrogate(typeof(SmartEnumNameSurrogate<TestEnumDouble, double>));
        }

        [Fact]
        public void SchemaValidation()
        {
            var result = model.GetSchema(typeof(TestEnumBoolean));

            result.Should().BeEquivalentTo(SchemaString);
        }

        [Fact]
        public void SerializesValue()
        {
            var result = Utils.DeepClone(TestInstance, model);

            result.Should().BeEquivalentTo(TestInstance);
        }

        [Fact]
        public void SerializesNull()
        {
            var result = Utils.DeepClone(NullTestInstance, model);
            
            result.Should().BeEquivalentTo(NullTestInstance);
        }    
    }
}