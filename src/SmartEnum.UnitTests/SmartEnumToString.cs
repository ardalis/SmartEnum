using Xunit;

namespace SmartEnum.UnitTests
{

    public class SmartEnumToString
    {
        [Fact]
        public void ReturnsFormattedNameValueAndDescription()
        {
            Assert.Equal("One (1) \"One (default)\"", TestEnum.One.ToString());
            Assert.Equal("Two (2)", TestEnum.Two.ToString());
            Assert.Equal("Three (3)", TestEnum.Three.ToString());
        }
    }
}
