using SmartEnum.Exceptions;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumFromValue
    {
        [Fact]
        public void ReturnsEnumGivenMatchingValue()
        {
            Assert.Equal(TestEnum.One, TestEnum.FromValue(1));
        }

        [Fact]
        public void ThrowsGivenNonMatchingValue()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => TestEnum.FromValue(-1));
        }

        [Fact]
        public void ReturnsDefaultEnumGivenNonMatchingValue()
        {
            var defaultEnum = TestEnum.One;

            Assert.Equal(defaultEnum, TestEnum.FromValue(-1, defaultEnum));
        }
    }
}