namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
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
        public void ThrowsGivenNonMatchingValue()
        {
            var value = string.Empty;

            Action action = () => TestStringEnum.FromValue(value);
            
            action.Should()
            .ThrowExactly<SmartEnumNotFoundException>()
            .WithMessage($"No {typeof(TestStringEnum).Name} with Value {value} found.");

        }

        [Fact]
        public void ReturnsDefaultEnumGivenNonMatchingValue()
        {
            var value = string.Empty;
            var defaultEnum = TestStringEnum.One;

            var result = TestStringEnum.FromValue(value, defaultEnum);

            result.Should().BeSameAs(defaultEnum);
        }
    }
}