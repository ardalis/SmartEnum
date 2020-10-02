using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.UnitTests;
using Xunit;

namespace Ardalis.SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumFromValue
    {
        [Theory]
        [InlineData(1, "One")]
        [InlineData(2, "Two")]
        [InlineData(3, "One, Two")]
        [InlineData(4, "Three")]
        [InlineData(5, "One, Three")]
        [InlineData(6, "Two, Three")]
        [InlineData(7, "One, Two, Three")]
        [InlineData(8, "Four")]
        [InlineData(9, "One, Four")]
        [InlineData(10, "Two, Four")]
        [InlineData(11, "One, Two, Four")]
        [InlineData(12, "Three, Four")]
        [InlineData(13, "One, Three, Four")]
        [InlineData(14, "Two, Three, Four")]
        [InlineData(15, "One, Two, Three, Four")]
        [InlineData(16, "Five")]
        [InlineData(17, "One, Five")]
        [InlineData(18, "Two, Five")]
        [InlineData(19, "One, Two, Five")]
        [InlineData(20, "Three, Five")]
        [InlineData(21, "One, Three, Five")]
        [InlineData(22, "Two, Three, Five")]
        [InlineData(23, "One, Two, Three, Five")]
        [InlineData(24, "Four, Five")]
        [InlineData(25, "One, Four, Five")]
        [InlineData(26, "Two, Four, Five")]
        [InlineData(27, "One, Two, Four, Five")]
        [InlineData(28, "Three, Four, Five")]
        [InlineData(29, "One, Three, Four, Five")]
        [InlineData(30, "Two, Three, Four, Five")]
        [InlineData(31, "One, Two, Three, Four, Five")]
        public void ReturnValidStringValues(int inputValue, string outputValue)
        {
            var result = SmartFlagTestEnum.FromValueToString(inputValue);
            Assert.Equal(outputValue, result);
        }

        [Fact]
        public void ReturnsIEnumerableWithSingleValidValue()
        {
            SmartFlagTestEnum.TryFromValue(1, out var TEnum);

            var list = TEnum.ToList();

            Assert.Single(TEnum);
            Assert.Equal("One", list[0].Name);
        }

        [Fact]
        public void ReturnsIEnumerableWithTwoValidValues()
        {
            SmartFlagTestEnum.TryFromValue(3, out var Tenum);

            var list = Tenum.ToList();

            Assert.Equal(2, Tenum.Count());
            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].Name);
        }

        [Fact]
        public void ReturnsStringWithFourValidValues()
        {
            var result = SmartFlagTestEnum.FromValueToString(15);

            Assert.Equal("One, Two, Three, Four", result);
        }

        [Fact]
        public void ReturnsStringWithSingleValidValue()
        {
            var result = SmartFlagTestEnum.FromValueToString(16);

            Assert.Equal("Five", result);
        }

        [Fact]
        public void ThrowsExceptionWhenEnumIsAboveAllowableRangeFromValueToString()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => SmartFlagTestEnum.FromValueToString(32));
        }

        [Fact]
        public void ThrowsExceptionWhenEnumIsAboveAllowableRangeFromValue()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => SmartFlagTestEnum.FromValue(32));
        }

        [Fact]
        public void DoesNothingIfValueEqualsMaximumValue_BoundaryValueTest()
        {
            var result = SmartFlagTestEnum.FromValue(31);
        }

        [Fact]
        public void ThrowsExceptionWhenEnumIsAboveAllowableRangeV2()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => SmartFlagTestEnumV2.FromValueToString(16));
        }

        [Fact]
        public void OrBitwiseOperatorReturnsIEnumerableWithTwoSameValues()
        {
            SmartFlagTestEnum.TryFromValue(SmartFlagTestEnum.One | SmartFlagTestEnum.Two, out var enumList);

            var list = enumList.ToList();

            Assert.Equal(2, enumList.Count());
            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].ToString());
        }

        [Fact]
        public void OrBitwiseOperatorReturnsIEnumerableWithThreeSameValues()
        {
            SmartFlagTestEnum.TryFromValue((SmartFlagTestEnum.One | SmartFlagTestEnum.Two | SmartFlagTestEnum.Three), out var output);

            var list = output.ToList();

            Assert.Equal("One", list[0].Name);
            Assert.Equal("Two", list[1].Name);
            Assert.Equal("Three", list[2].Name);

        }

        [Fact]
        public void OrBitwiseOperatorReturnsStringWithThreeSameValues()
        {
            var result = SmartFlagTestEnum.FromValueToString((SmartFlagTestEnum.One | SmartFlagTestEnum.Two | SmartFlagTestEnum.Three));

            Assert.Equal("One, Two, Three", result);
        }

        [Fact]
        public void OrBitwiseOnNonIntTypeWorks()
        {
            var result = FlagEnumDecimal.FromValue((int)FlagEnumDecimal.One | (int)FlagEnumDecimal.Two).ToList();

            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void SmartFlagEnumWithZeroValueReturnsValidValues()
        {
            var result = SmartFlagTestEnum.FromValue(3).ToList();

            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void ZeroValueReturnsIEnumerableWithZeroValueEnum()
        {
            var result = SmartFlagTestEnum.FromValue(0).ToList();

            Assert.Equal("Zero", result[0].Name);
        }

        [Fact]
        public void DecimalInputReturnsValidIEnumerableWithTwoValues()
        {
            var result = FlagEnumDecimal.FromValue(3M).ToList();

            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void TryFromValueToStringReturnValidString()
        {
            var boolReturn = SmartFlagTestEnum.TryFromValueToString(3, out var result);

            Assert.True(boolReturn);
            Assert.Equal("One, Two", result);
        }

        [Fact]
        public void TryFromValueToStringReturnsNullGivenInvalidValue()
        {
            var boolReturn = SmartFlagTestEnum.TryFromValueToString(345, out var result);

            Assert.False(boolReturn);
            Assert.Null(result);
        }

        [Fact]
        public void FromValueHighNumberToStringThrowsSmartEnumNotFoundException()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => SmartFlagTestEnum.FromValueToString(124));
        }
    }
}

