using SmartEnum.UnitTests.TestEnums;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumExplicitConversion
    {
        [Fact]
        public void ReturnsEnumFromGivenValue()
        {
            var result = (TestEnum) 1;

            Assert.Equal(TestEnum.One, result);
        }
    }
}
