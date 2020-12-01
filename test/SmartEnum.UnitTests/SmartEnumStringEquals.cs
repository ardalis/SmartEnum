namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;
    using Ardalis.SmartEnum.UnitTests.TestData;

    public class SmartEnumStringEquals
    {
        public static TheoryData<TestStringEnum, object, bool> EqualsTestEnumObjectData =>
            new TheoryData<TestStringEnum, object, bool> 
            {
                { TestStringEnum.One, null, false },
                { TestStringEnum.One, TestEnum.One, false },
                { TestStringEnum.One, TestStringEnum.One, true },
                { TestStringEnum.One, TestStringEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualsTestEnumObjectData))]
        public void EqualsObjectReturnsExpected(TestStringEnum left, object right, bool expected)
        {
            var result = left.Equals(right);

            result.Should().Be(expected);
        }

        public static TheoryData<TestStringEnum, TestStringEnum, bool> EqualsSmartEnumData =>
            new TheoryData<TestStringEnum, TestStringEnum, bool> 
            {
                { TestStringEnum.One, null, false },
                { TestStringEnum.One, TestStringEnum.One, true },
                { TestStringEnum.One, TestStringEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualsSmartEnumData))]
        public void EqualsSmartEnumReturnsExpected(TestStringEnum left, TestStringEnum right, bool expected)
        {
            var result = left.Equals(right);

            result.Should().Be(expected);
        }

        public static TheoryData<TestStringEnum, TestStringEnum, bool> EqualOperatorData =>
            new TheoryData<TestStringEnum, TestStringEnum, bool> 
            {
                { null, null, true },
                { null, TestStringEnum.One, false },
                { TestStringEnum.One, null, false },
                { TestStringEnum.One, TestStringEnum.One, true },
                { TestStringEnum.One, TestStringEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void EqualOperatorReturnsExpected(TestStringEnum left, TestStringEnum right, bool expected)
        {
            var result = left == right;

            result.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void NotEqualOperatorReturnsExpected(TestStringEnum left, TestStringEnum right, bool expected)
        {
            var result = left != right;

            result.Should().Be(!expected);
        }
    }
} 