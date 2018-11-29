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
    }
}
