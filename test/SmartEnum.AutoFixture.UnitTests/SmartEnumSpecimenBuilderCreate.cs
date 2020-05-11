namespace Ardalis.SmartEnum.AutoFixture.UnitTests
{
    using System;
    using global::AutoFixture;
    using FluentAssertions;
    using Xunit;

    public class SmartEnumSpecimenBuilderCreate
    {
        [Fact]
        public void ReturnsEnumGivenNoExplicitPriorUse()
        {
            var fixture = new Fixture()
                .Customize(new SmartEnumCustomization());

            var result = fixture.Create<TestEnum>();

            result.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsSmartEnumGivenNoExplicitPriorUse()
        {
            var fixture = new Fixture()
                .Customize(new SmartEnumCustomization());

            var result = fixture.Create<SmartFlagTestEnum>();

            result.Should().BeSameAs(SmartFlagTestEnum.One);
        }
    }
}