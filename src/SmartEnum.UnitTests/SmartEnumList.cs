using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumList
    {
        [Fact]
        public void ReturnsAllDefinedSmartEnums()
        {
            var result = TestEnum.List;

            Assert.Equal(3, result.Count);
            Assert.Contains(TestEnum.One, result);
            Assert.Contains(TestEnum.Two, result);
            Assert.Contains(TestEnum.Three, result);
        }
    }
}
