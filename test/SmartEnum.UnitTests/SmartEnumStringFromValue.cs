namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using System;
    using Xunit;

    public class SmartEnumStringFromValue
    {
        public static TheoryData<string, TestStringEnum> TestStringEnumData =>
            new TheoryData<string, TestStringEnum>
            {
                { nameof(TestStringEnum.One), TestStringEnum.One },
                { nameof(TestStringEnum.Two), TestStringEnum.Two },
            };

        [Theory]
        [MemberData(nameof(TestStringEnumData))]
        public void ReturnsEnumGivenMatchingValue(string value, TestStringEnum expected)
        {
            var result = TestStringEnum.FromValue(value);

            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void ReturnsEnumGivenMatchingNullValue()
        {
            var result = TestNullableStringEnum.FromValue(null);

            result.Should().BeSameAs(TestNullableStringEnum.None);
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData(null)]
        public void ThrowsGivenNonMatchingValue(string value)
        {
            Action action = () => TestStringEnum.FromValue(value);

            action.Should()
            .ThrowExactly<SmartEnumNotFoundException>()
            .WithMessage($"No {typeof(TestStringEnum).Name} with Value {value} found.");

        }

        [Fact]
        public void ReturnsDefaultEnumGivenNonMatchingValue()
        {
            var value = "invalid";
            var defaultEnum = TestStringEnum.One;

            var result = TestStringEnum.FromValue(value, defaultEnum);

            result.Should().BeSameAs(defaultEnum);
        }


        [Fact]
        public void ReturnsDerivedEnumByValue()
        {
            TestBaseEnumWithDerivedValues result = DerivedTestEnumWithValues1.FromValue(1);

            Assert.Equal(DerivedTestEnumWithValues1.A, result);
        }
    }
}
