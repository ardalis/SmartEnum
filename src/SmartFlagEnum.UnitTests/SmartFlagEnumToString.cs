using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using SmartEnum.UnitTests;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumToString
    {
        public static TheoryData<SmartFlagTestEnum> NameData =>
            new TheoryData<SmartFlagTestEnum>
            {
                SmartFlagTestEnum.One,
                SmartFlagTestEnum.Two,
                SmartFlagTestEnum.Three,
            };

        [Theory]
        [MemberData(nameof(NameData))]
        public void ReturnsFormattedNameAndValue(SmartFlagTestEnum smartEnum)
        {
            var result = smartEnum.ToString();

            result.Should().Be(smartEnum.Name);
        }
    }
}
