namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;

    public class SmartEnumTryFromValue
    {
        [Fact]
        public void ReturnsTrueAndEnumGivenMatchingValue()
        {
            var result = TestEnum.TryFromValue(1, out var output);

            result.Should().BeTrue();
            output.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsFalseAndNullGivenNonMatchingValue()
        {
            var result = TestEnum.TryFromValue(-1, out var output);

            result.Should().BeFalse();
            output.Should().BeNull();
        }

        [Fact]
        public void ReturnsFalseAndNullGivenNullStringValue()
        {
            var result = TestStringEnum.TryFromValue(null, out var output);

            result.Should().BeFalse();
            output.Should().BeNull();
        }

        [Fact]
        public void ReturnsTrueAndEnumGivenMatchingStringValue()
        {
            var result = TestStringEnum.TryFromValue(nameof(TestStringEnum.One), out var output);

            result.Should().BeTrue();
            output.Should().BeSameAs(TestStringEnum.One);
        }

        [Fact]
        public void ReturnsTrueAndEnumGivenMatchingNullValue()
        {
            var result = TestNullableStringEnum.TryFromValue(null, out var output);

            result.Should().BeTrue();
            output.Should().BeSameAs(TestNullableStringEnum.None);
        }
    }
}
