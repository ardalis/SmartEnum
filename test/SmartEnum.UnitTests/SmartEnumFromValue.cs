namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;
    using Ardalis.SmartEnum.UnitTests.TestData;

    public class SmartEnumFromValue
    {
        [Fact]
        public void ReturnsEnumGivenMatchingValue()
        {
            var result = TestEnum.FromValue(1);

            result.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsEnumGivenDerivedClass()
        {
            var result = TestDerivedEnum.FromValue(1);

            result.Should().NotBeNull().And.BeSameAs(TestDerivedEnum.One);
        }

        [Fact]
        public void ThrowsGivenNonMatchingValue()
        {
            var value = -1;

            Action action = () => TestEnum.FromValue(value);
            
            action.Should()
            .ThrowExactly<SmartEnumNotFoundException>()
            .WithMessage($"No {typeof(TestEnum).Name} with Value {value} found.");

        }

        [Fact]
        public void ReturnsDefaultEnumGivenNonMatchingValue()
        {
            var value = -1;
            var defaultEnum = TestEnum.One;

            var result = TestEnum.FromValue(value, defaultEnum);

            result.Should().BeSameAs(defaultEnum);
        }
    }
}