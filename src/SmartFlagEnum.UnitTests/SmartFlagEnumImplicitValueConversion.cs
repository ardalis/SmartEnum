using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using SmartEnum.UnitTests;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumImplicitValueConversion
    {
        [Fact]
        public void ReturnsValueOfGivenEnum()
        {
            var smartFlagEnum = SmartFlagTestEnum.One;

            int result = smartFlagEnum;

            result.Should().Be(smartFlagEnum.Value);
        }
    }
}
