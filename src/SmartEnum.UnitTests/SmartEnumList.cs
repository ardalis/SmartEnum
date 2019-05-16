using SmartEnum.UnitTests.TestEnums;
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

        [Fact]
        public void ReturnsAllEnumsFromDerivedTypes()
        {
            var result = BaseTestEnum.List;

            Assert.Equal(4, result.Count);
            Assert.Contains(DerivedTestEnum1.A, result);
            Assert.Contains(DerivedTestEnum1.B, result);
            Assert.Contains(DerivedTestEnum2.C, result);
            Assert.Contains(DerivedTestEnum2.D, result);
        }
    }
}
