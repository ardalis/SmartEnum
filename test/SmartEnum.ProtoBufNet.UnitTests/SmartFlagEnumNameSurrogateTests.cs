using FluentAssertions;
using ProtoBuf;
using ProtoBuf.Meta;
using Xunit;

namespace Ardalis.SmartEnum.ProtoBufNet.UnitTests
{
    public class SmartFlagEnumNameSurrogateTests
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

        private static readonly TestClass NullTestInstance = new TestClass
        {
            Int16 = null,
            Int32 = null,
            Double = null
        };

        private static readonly TestClass TestInstance = new TestClass
        {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance2,
            Double = FlagTestEnumDouble.Instance
        };

        readonly string SchemaString =
            @"syntax = ""proto2"";
package Ardalis.SmartEnum.ProtoBufNet;

message SmartFlagEnumNameSurrogate_FlagTestEnumInt32_Int32 {
   required string Name = 1;
}
";

        readonly RuntimeTypeModel model;

        public SmartFlagEnumNameSurrogateTests()
        {
            model = RuntimeTypeModel.Create();
            model.Add(typeof(FlagTestEnumInt16), false).SetSurrogate(typeof(SmartFlagEnumNameSurrogate<FlagTestEnumInt16, short>));
            model.Add(typeof(FlagTestEnumInt32), false).SetSurrogate(typeof(SmartFlagEnumNameSurrogate<FlagTestEnumInt32, int>));
            model.Add(typeof(FlagTestEnumDouble), false).SetSurrogate(typeof(SmartFlagEnumNameSurrogate<FlagTestEnumDouble, double>));
        }

        [Fact]
        public void SchemaValidation()
        {
            var result = model.GetSchema(typeof(FlagTestEnumInt32));

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
