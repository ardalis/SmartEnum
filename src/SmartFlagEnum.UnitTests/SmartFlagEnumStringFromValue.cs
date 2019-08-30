using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.Exceptions;
using FluentAssertions;
using SmartEnum.UnitTests;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumStringFromValue
    {

        [Fact]
        public void ThrowsGivenNonMatchingValue()
        {
            var value = string.Empty;

            Action action = () => SmartFlagTestStringEnum.FromValue(value);

            action.Should()
                .ThrowExactly<InvalidFlagEnumValueParseException>()
                .WithMessage($"The value: {value.ToString()} input to {nameof(SmartFlagTestStringEnum)} could not be parsed into an integer value.");
        }

        [Fact]
        public void ReturnsDefaultEnumGivenNonMatchingValue()
        {
            var value = string.Empty;
            var defaultEnumValue = new List<SmartFlagTestStringEnum>{SmartFlagTestStringEnum.One, SmartFlagTestStringEnum.Two};

            var result = SmartFlagTestStringEnum.FromValue(value, defaultEnumValue);

            result.Should().BeSameAs(defaultEnumValue);
        }
    }
}
