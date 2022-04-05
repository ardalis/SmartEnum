namespace Ardalis.SmartEnum.ProtoBufNet.UnitTests
{
    using ProtoBuf;
    using ProtoBuf.Meta;
    using Xunit;
    using FluentAssertions;

    public class SmartEnumValueSurrogateTests
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

        static readonly TestClass NullTestInstance = new TestClass
        {
            Bool = null,
            Int16 = null,
            Int32 = null,
            Double = null,
        };

        static readonly TestClass TestInstance = new TestClass
        {
            Bool = TestEnumBoolean.Instance,
            Int16 = TestEnumInt16.Instance,
            Int32 = TestEnumInt32.Instance,
            Double = TestEnumDouble.Instance,
        };

        readonly string SchemaString =
@"syntax = ""proto3"";
package Ardalis.SmartEnum.ProtoBufNet;

message SmartEnumValueSurrogate_TestEnumBoolean_Boolean {
   bool Value = 1;
}
";

        readonly RuntimeTypeModel model;

        public SmartEnumValueSurrogateTests()
        {
            model = RuntimeTypeModel.Create();
            model.Add(typeof(TestEnumBoolean), false).SetSurrogate(typeof(SmartEnumValueSurrogate<TestEnumBoolean, bool>));
            model.Add(typeof(TestEnumInt16), false).SetSurrogate(typeof(SmartEnumValueSurrogate<TestEnumInt16, short>));
            model.Add(typeof(TestEnumInt32), false).SetSurrogate(typeof(SmartEnumValueSurrogate<TestEnumInt32, int>));
            model.Add(typeof(TestEnumDouble), false).SetSurrogate(typeof(SmartEnumValueSurrogate<TestEnumDouble, double>));
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