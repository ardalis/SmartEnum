namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;

    public class SmartEnumList
    {
        [Fact]
        public void ReturnsAllDefinedSmartEnums()
        {
            var result = TestEnum.List;

            result.Should().BeEquivalentTo(new[] {
                TestEnum.One,
                TestEnum.Two,
                TestEnum.Three,
            });
        }

        [Fact]
        public void ReturnsAllBaseAndDerivedSmartEnums()
        {
            var result = TestBaseEnumWithDerivedValues.List;

            result.Should().BeEquivalentTo(DerivedTestEnumWithValues1.A, DerivedTestEnumWithValues1.B, DerivedTestEnumWithValues2.C, DerivedTestEnumWithValues2.D);
        }
    }
}
