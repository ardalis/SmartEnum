namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class SmartEnumEquals
    {
        private class TestEnum2 : SmartEnum<TestEnum2, int>
        {
            public static TestEnum2 One = new TestEnum2(nameof(One), 1);
            protected TestEnum2(string name, int value) : base(name, value)
            {
            }
        }

        public static TheoryData<TestEnum, object, bool> EqualsObjectData =>
            new TheoryData<TestEnum, object, bool> 
            {
                { TestEnum.One, null, false },
                { TestEnum.One, TestEnum.One, true },
                { TestEnum.One, TestEnum2.One, false },
                { TestEnum.One, TestEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualsObjectData))]
        public void EqualsObjectReturnsExpected(TestEnum left, object right, bool expected)
        {
            var result = left.Equals(right);

            result.Should().Be(expected);
        }

        public static TheoryData<TestEnum, TestEnum, bool> EqualsSmartEnumData =>
            new TheoryData<TestEnum, TestEnum, bool> 
            {
                { TestEnum.One, null, false },
                { TestEnum.One, TestEnum.One, true },
                { TestEnum.One, TestEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualsSmartEnumData))]
        public void EqualsSmartEnumReturnsExpected(TestEnum left, object right, bool expected)
        {
            var result = left.Equals(right);

            result.Should().Be(expected);
        }

        public static TheoryData<TestEnum, TestEnum, bool> EqualOperatorData =>
            new TheoryData<TestEnum, TestEnum, bool> 
            {
                { null, null, true },
                { null, TestEnum.One, false },
                { TestEnum.One, null, false },
                { TestEnum.One, TestEnum.One, true },
                { TestEnum.One, TestEnum.Two, false },
            };

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void EqualOperatorReturnsExpected(TestEnum left, TestEnum right, bool expected)
        {
            var result = left == right;

            result.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void NotEqualOperatorReturnsExpected(TestEnum left, TestEnum right, bool expected)
        {
            var result = left != right;

            result.Should().Be(!expected);
        }
    }
} 