namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;

    public class SmartEnumEnumerateList
    {
        [Fact]
        public void ReturnsSameValuesAsListForAllDefinedSmartEnums()
        {
            var result = TestEnum.EnumerateList();

            result.Should().BeEquivalentTo(TestEnum.List);
        }

        [Fact]
        public void ReturnsSameValuesAsListForAllBaseAndDerivedSmartEnums()
        {
            var result = TestBaseEnumWithDerivedValues.EnumerateList();

            result.Should().BeEquivalentTo(TestBaseEnumWithDerivedValues.List);
        }
    }
}
