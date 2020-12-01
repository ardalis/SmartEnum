namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;
    using Ardalis.SmartEnum.UnitTests.TestData;

    public class SmartEnumToString
    {
        public static TheoryData<TestEnum> NameData =>
            new TheoryData<TestEnum> 
            {
                TestEnum.One,
                TestEnum.Two,
                TestEnum.Three, 
            };

        [Theory]
        [MemberData(nameof(NameData))]
        public void ReturnsFormattedNameAndValue(TestEnum smartEnum)
        {
            var result = smartEnum.ToString();

            result.Should().Be(smartEnum.Name);
        }
    }
}
