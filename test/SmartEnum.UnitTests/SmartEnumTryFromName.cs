namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;
    using Ardalis.SmartEnum.UnitTests.TestData;

    public class SmartEnumTryFromName
    {
        [Fact]
        public void ReturnsTrueGivenMatchingName()
        {
            var result = TestEnum.TryFromName("One", out var _);

            result.Should().BeTrue();
        }

        [Fact]
        public void ProducesEnumGivenMatchingName()
        {
            TestEnum.TryFromName("One", out var result);

            result.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsFalseGivenEmptyString()
        {
            var result = TestEnum.TryFromName(String.Empty, out var _);

            result.Should().BeFalse();
        }

        [Fact]
        public void ProducesNullGivenEmptyString()
        {
            TestEnum.TryFromName(String.Empty, out var result);

            result.Should().BeNull();
        }

        [Fact]
        public void ReturnsFalseGivenNull()
        {
            var result = TestEnum.TryFromName(null, out var _);

            result.Should().BeFalse();
        }

        [Fact]
        public void ProducesNullGivenNull()
        {
            TestEnum.TryFromName(null, out var result);

            result.Should().BeNull();
        }
    }
}