using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum.MessagePack;
using Ardalis.SmartEnum.MessagePack.UnitTests;
using FluentAssertions;
using MessagePack;
using MessagePack.Resolvers;
using Xunit;

namespace Ardalis.SmartEnum.MessagePack.UnitTests
{
    public class SmartFlagEnumValueFormatterTests
    {
        [MessagePackObject]
        public class FlagTestClass
        {
            [Key(0)]
            [MessagePackFormatter(typeof(SmartFlagEnumValueFormatter<FlagTestEnumInt16, short>))]
            public FlagTestEnumInt16 Int16 { get; set; }

            [Key(1)]
            [MessagePackFormatter(typeof(SmartFlagEnumValueFormatter<FlagTestEnumInt32, int>))]
            public FlagTestEnumInt32 Int32 { get; set; }

            [Key(2)]
            [MessagePackFormatter(typeof(SmartFlagEnumValueFormatter<FlagTestEnumDouble, double>))]
            public FlagTestEnumDouble Double { get; set; }
        }

        static readonly FlagTestClass NullFlagTestInstance = new FlagTestClass()
        {
            Int16 = null,
            Int32 = null,
            Double = null,
        };

        static readonly FlagTestClass FlagTestInstance = new FlagTestClass()
        {
            Int16 = FlagTestEnumInt16.Instance,
            Int32 = FlagTestEnumInt32.Instance2,
            Double = FlagTestEnumDouble.Instance,
        };

        static readonly string JsonString = @"[1,2,1]";

        static SmartFlagEnumValueFormatterTests()
        {
            CompositeResolver.Create(
                new SmartFlagEnumValueFormatter<FlagTestEnumInt16, short>(),
                new SmartFlagEnumValueFormatter<FlagTestEnumInt32, int>(),
                new SmartFlagEnumValueFormatter<FlagTestEnumDouble, double>()
            );
        }

        [Fact]
        public void SerializesValue()
        {
            var message = MessagePackSerializer.Serialize(FlagTestInstance);

            MessagePackSerializer.ConvertToJson(message).Should().Be(JsonString);
        }

        [Fact]
        public void DeserializesValue()
        {
            var message = MessagePackSerializer.Serialize(FlagTestInstance);

            var obj = MessagePackSerializer.Deserialize<FlagTestClass>(message);

            obj.Int16.Should().BeSameAs(FlagTestEnumInt16.Instance);
            obj.Int32.Should().BeSameAs(FlagTestEnumInt32.Instance2);
            obj.Double.Should().BeSameAs(FlagTestEnumDouble.Instance);
        }
    }
}
