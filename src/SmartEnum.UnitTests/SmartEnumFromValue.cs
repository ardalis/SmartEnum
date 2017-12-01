using Ardalis.SmartEnum;
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

        // TODO: test non-matching case
    }
}
