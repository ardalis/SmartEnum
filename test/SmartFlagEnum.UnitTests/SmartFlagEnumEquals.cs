using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.UnitTests;
using FluentAssertions;
using Xunit;

namespace Ardalis.SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumEquals
    {
        private class TestFlagEnum2 : SmartFlagEnum<TestFlagEnum2, int>
        {
            public static TestFlagEnum2 One = new TestFlagEnum2(nameof(One), 1);
            protected TestFlagEnum2(string name, int value) : base(name, value)
            {
            }
        }

        public static TheoryData<SmartFlagTestEnum, object, bool> EqualsObjectData =>
            new TheoryData<SmartFlagTestEnum, object, bool>
            {
                { SmartFlagTestEnum.One, null, false },
                { SmartFlagTestEnum.One, SmartFlagTestEnum.One, true },
                { SmartFlagTestEnum.One, TestFlagEnum2.One, false },
                { SmartFlagTestEnum.One, SmartFlagTestEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualsObjectData))]
        public void EqualsObjectReturnsExpected(SmartFlagTestEnum left, object right, bool expected)
        {
            var result = left.Equals(right);

            result.Should().Be(expected);
        }

        public static TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, bool> EqualsSmartEnumData =>
            new TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, bool>
            {
                { SmartFlagTestEnum.One, null, false },
                { SmartFlagTestEnum.One, SmartFlagTestEnum.One, true },
                { SmartFlagTestEnum.One, SmartFlagTestEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualsSmartEnumData))]
        public void EqualsSmartEnumReturnsExpected(SmartFlagTestEnum left, object right, bool expected)
        {
            var result = left.Equals(right);

            result.Should().Be(expected);
        }

        public static TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, bool> EqualOperatorData =>
            new TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, bool>
            {
                { null, null, true },
                { null, SmartFlagTestEnum.One, false },
                { SmartFlagTestEnum.One, null, false },
                { SmartFlagTestEnum.One, SmartFlagTestEnum.One, true },
                { SmartFlagTestEnum.One, SmartFlagTestEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void EqualOperatorReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, bool expected)
        {
            var result = left == right;

            result.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void NotEqualOperatorReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, bool expected)
        {
            var result = left != right;

            result.Should().Be(!expected);
        }
    }
}
