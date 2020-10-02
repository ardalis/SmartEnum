using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum.UnitTests;
using FluentAssertions;
using Xunit;

namespace Ardalis.SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumCompareTo
    {
        public static TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, int> CompareToData =>
           new TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, int>
           {
                { SmartFlagTestEnum.Two, SmartFlagTestEnum.One, 1 },
                { SmartFlagTestEnum.Two, SmartFlagTestEnum.Two, 0 },
                { SmartFlagTestEnum.Two, SmartFlagTestEnum.Three, -1 },
           };

        [Theory]
        [MemberData(nameof(CompareToData))]
        public void CompareToReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, int expected)
        {
            var result = left.CompareTo(right);

            result.Should().Be(expected);
        }

        public static TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, bool, bool, bool> ComparisonOperatorsData =>
            new TheoryData<SmartFlagTestEnum, SmartFlagTestEnum, bool, bool, bool>
            {
                { SmartFlagTestEnum.Two, SmartFlagTestEnum.One, false, false, true },
                { SmartFlagTestEnum.Two, SmartFlagTestEnum.Two, false, true, false },
                { SmartFlagTestEnum.Two, SmartFlagTestEnum.Three, true, false, false },
            };

#pragma warning disable xUnit1026 // Theory method does not use parameter

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void LessThanReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left < right;

            result.Should().Be(lessThan);
        }

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void LessThanOrEqualReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left <= right;

            result.Should().Be(lessThan || equalTo);
        }

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void GreaterThanReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left > right;

            result.Should().Be(greaterThan);
        }

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void GreaterThanOrEqualReturnsExpected(SmartFlagTestEnum left, SmartFlagTestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left >= right;

            result.Should().Be(greaterThan || equalTo);
        }

#pragma warning restore xUnit1026 // Theory method does not use parameter
    }
}
