using SmartEnum.UnitTests.TestEnums;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumImplicitValueConversion
    {
        [Fact]
        public void ReturnsValueOfGivenEnum()
        {
            int result = TestEnum.One;

            Assert.Equal(TestEnum.One.Value, result);
        }
    }
}
