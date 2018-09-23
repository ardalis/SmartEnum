using System;
using AutoFixture;
using Xunit;

namespace Ardalis.SmartEnum.AutoFixture.UnitTests
{
    public class SmartEnumSpecimenBuilderCreate
    {
        [Fact]
        public void ReturnsEnumGivenNoExplicitPriorUse()
        {
            string expected = "One";
            IFixture fixture =  new Fixture()
                .Customize(new SmartEnumCustomization());
            Assert.Equal(expected, fixture.Create<TestEnum>().Name);
        }
    }
}