using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumFromConstructor
    {
        [Fact]
        public void ShouldEqualMatchingNewEnumInstance()
        {
            var expected = TestEnum.One;

            Assert.Equal(expected, TestEnum.CreateFromConstructor(expected.Name, expected.Value));
        }

        [Fact]
        public void ShouldNotEqualNonMatchingNewEnumInstance()
        {
            var expected = TestEnum.One;

            Assert.NotEqual(expected, TestEnum.CreateFromConstructor("Two", 2));            
        }
    }
}
