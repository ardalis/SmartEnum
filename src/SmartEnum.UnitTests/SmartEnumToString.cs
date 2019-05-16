using SmartEnum.UnitTests.TestEnums;
using Xunit;

namespace SmartEnum.UnitTests
{

    public class SmartEnumToString
    {
        [Fact]
        public void ReturnsFormattedNameAndValue()
        {
            Assert.Equal("One (1)", TestEnum.One.ToString());
            Assert.Equal("Two (2)", TestEnum.Two.ToString());
            Assert.Equal("Three (3)", TestEnum.Three.ToString());
        }
    }
}
