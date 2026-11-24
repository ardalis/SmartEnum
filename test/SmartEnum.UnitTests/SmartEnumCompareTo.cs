namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using System;
    using Xunit;

    public class SmartEnumCompareTo
    {
        public static TheoryData<TestEnum, TestEnum, int> CompareToData =>
            new TheoryData<TestEnum, TestEnum, int>
            {
                { TestEnum.Two, TestEnum.One, 1 },
                { TestEnum.Two, TestEnum.Two, 0 },
                { TestEnum.Two, TestEnum.Three, -1 },
            };

        [Theory]
        [MemberData(nameof(CompareToData))]
        public void CompareToReturnsExpected(TestEnum left, TestEnum right, int expected)
        {
            var result = left.CompareTo(right);

            result.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(CompareToData))]
        public void NonGenericCompareToReturnsExpected(TestEnum left, TestEnum right, int expected)
        {
            var comparable = AsNonGenericComparable(left);

            comparable.CompareTo(right).Should().Be(expected);
        }

        [Fact]
        public void NonGenericCompareToNullReturnsOne()
        {
            var comparable = AsNonGenericComparable(TestEnum.Two);

            comparable.CompareTo(null).Should().Be(1);
        }

        [Fact]
        public void NonGenericCompareToNonSmartEnumThrows()
        {
            var comparable = AsNonGenericComparable(TestEnum.Two);

            Action act = () => comparable.CompareTo(new object());

            act.Should().Throw<ArgumentException>().WithParameterName("obj");
        }

        // Cast via 'as' so a missing non-generic IComparable fails at runtime with an assertion,
        // rather than breaking compilation of the whole test project.
        private static IComparable AsNonGenericComparable(object value)
        {
            var comparable = value as IComparable;
            comparable.Should().NotBeNull("SmartEnum should implement non-generic IComparable");
            return comparable;
        }

        public static TheoryData<TestEnum, TestEnum, bool, bool, bool> ComparisonOperatorsData =>
            new TheoryData<TestEnum, TestEnum, bool, bool, bool>
            {
                { TestEnum.Two, TestEnum.One, false, false, true },
                { TestEnum.Two, TestEnum.Two, false, true, false },
                { TestEnum.Two, TestEnum.Three, true, false, false },
            };

        #pragma warning disable xUnit1026 // Theory method does not use parameter

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void LessThanReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left < right;

            result.Should().Be(lessThan);
        }

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void LessThanOrEqualReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left <= right;

            result.Should().Be(lessThan || equalTo);
        }

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void GreaterThanReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left > right;

            result.Should().Be(greaterThan);
        }

        [Theory]
        [MemberData(nameof(ComparisonOperatorsData))]
        public void GreaterThanOrEqualReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
        {
            var result = left >= right;

            result.Should().Be(greaterThan || equalTo);
        }

        #pragma warning restore xUnit1026 // Theory method does not use parameter
    }
}