﻿namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;

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
