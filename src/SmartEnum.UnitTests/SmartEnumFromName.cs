using Ardalis.SmartEnum;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumFromName
    {
        [Fact]
        public void ReturnsEnumGivenMatchingName()
        {
            Assert.Equal(TestEnum.One, TestEnum.FromName("One"));
        }

        // TODO: test non-matching case
    }
}
