namespace Ardalis.SmartEnum.MessagePack.UnitTests
{
    using global::MessagePack;
    using global::MessagePack.Resolvers;
    using Xunit;
    using FluentAssertions;

    public class SmartEnumNameFormatterTests
    {
        [MessagePackObject]
        public class TestClass
        {
            [Key(0)]
            [MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumBoolean, bool>))]
            public TestEnumBoolean Bool { get; set; }

            [Key(1)]
            [MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumInt16, short>))]
            public TestEnumInt16 Int16 { get; set; }

            [Key(2)]
            [MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumInt32, int>))]
            public TestEnumInt32 Int32 { get; set; }

            [Key(3)]
            [MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumDouble, double>))]
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

        static readonly string JsonString = @"[""Instance"",""Instance"",""Instance"",""Instance""]";

        static SmartEnumNameFormatterTests()
        {
            CompositeResolver.Create(
                new SmartEnumNameFormatter<TestEnumBoolean, bool>(),
                new SmartEnumNameFormatter<TestEnumInt16, short>(),
                new SmartEnumNameFormatter<TestEnumInt32, int>(),
                new SmartEnumNameFormatter<TestEnumDouble, double>()
            );
        }

        [Fact]
        public void SerializesValue()
        {
            var message = MessagePackSerializer.Serialize(TestInstance);

            MessagePackSerializer.ConvertToJson(message).Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesValue()
        {
            var message = MessagePackSerializer.Serialize(TestInstance);

            var obj = MessagePackSerializer.Deserialize<TestClass>(message);

            obj.Bool.Should().BeSameAs(TestEnumBoolean.Instance);
            obj.Int16.Should().BeSameAs(TestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(TestEnumInt32.Instance);
            obj.Double.Should().BeSameAs(TestEnumDouble.Instance);
        }
    }
}