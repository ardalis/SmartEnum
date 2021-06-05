namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;

    public class SmartEnumExplicitConversion
    {
        [Fact]
        public void ReturnsEnumFromGivenValue()
        {
            int value = 1;

            var result = (TestEnum)value;

            result.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsEnumFromGivenNullableValue()
        {
            int? value = 1;

            var result = (TestEnum)value;

            result.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsEnumFromGivenNullableValueAsNull()
        {
            int? value = null;

            var result = (TestEnum)value;

            result.Should().BeNull();
        }
    }
}