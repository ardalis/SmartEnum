namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;
    using Ardalis.SmartEnum.UnitTests.TestData;

    public class SmartEnumImplicitValueConversion
    {
        [Fact]
        public void ReturnsValueOfGivenEnum()
        {
            var smartEnum = TestEnum.One;

            int result = smartEnum;

            result.Should().Be(smartEnum.Value);
        }
    }
}
