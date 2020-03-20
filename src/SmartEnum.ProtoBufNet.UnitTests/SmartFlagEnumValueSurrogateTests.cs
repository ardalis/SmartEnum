using FluentAssertions;
using ProtoBuf;
using ProtoBuf.Meta;
using SmartEnum.ProtoBufNet.UnitTests;
using Xunit;

namespace Ardalis.SmartEnum.ProtoBufNet.UnitTests
{
    public class SmartFlagEnumValueSurrogateTests
    {
        [ProtoContract]
        public class TestClass
        {
            [ProtoMember(1)]
            public FlagTestEnumInt16 Int16 { get; set; }

            [ProtoMember(2)]
            public FlagTestEnumInt32 Int32 { get; set; }

            [ProtoMember(3)] 
            public FlagTestEnumDouble Double { get; set; }
        }

        static readonly TestClass NullTestInstance = new TestClass
        {
            Int16 = null,
            Int32 = null,
            Double = null,
        };

        private static readonly TestClass TestInstance = new TestClass()
        {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance,
            Double = FlagTestEnumDouble.Instance
        };

        readonly string SchemaString =
            @"syntax = ""proto2"";
package Ardalis.SmartEnum.ProtoBufNet;

message SmartFlagEnumValueSurrogate_FlagTestEnumInt32_Int32 {
   required int32 Value = 1;
}
";

        private readonly RuntimeTypeModel model;

        public SmartFlagEnumValueSurrogateTests()
        {
            model = RuntimeTypeModel.Create();
            model.Add(typeof(FlagTestEnumInt16), false)
                .SetSurrogate(typeof(SmartFlagEnumValueSurrogate<FlagTestEnumInt16, short>));
            model.Add(typeof(FlagTestEnumInt32), false)
                .SetSurrogate(typeof(SmartFlagEnumValueSurrogate<FlagTestEnumInt32, int>));
            model.Add(typeof(FlagTestEnumDouble), false)
                .SetSurrogate(typeof(SmartFlagEnumValueSurrogate<FlagTestEnumDouble, double>));
        }

        [Fact]
        public void SchemaValidation()
        {
            var result = model.GetSchema(typeof(FlagTestEnumInt32));

            result.Should().BeEquivalentTo(SchemaString);
        }

        [Fact]
        public void SerializesAndDeserializesValue()
        {
            var result = Utils.DeepClone(TestInstance, model);

            result.Should().BeEquivalentTo(TestInstance);
        }

        [Fact]
        public void SerializeNull()
        {
            var result = Utils.DeepClone(NullTestInstance, model);

            result.Should().BeEquivalentTo(NullTestInstance);
        }
    }
}