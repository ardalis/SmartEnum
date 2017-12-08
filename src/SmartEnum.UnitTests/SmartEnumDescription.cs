using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumDescription
    {
        [Fact]
        public void DescriptionMatchesProvidedDescription()
        {
            Assert.Equal("One (default)", TestEnum.One.Description);
        }

        [Fact]
        public void DescriptionMatchesNameWhenDescriptionNotProvided()
        {
            Assert.Equal("Two", TestEnum.Two.Description);
        }
    }
}
