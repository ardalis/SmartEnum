using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.Exceptions;
using SmartEnum.UnitTests;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    public class SmartFlagGetFlagEnumTests
    {
        [Fact]
        public void DoesNothingIfEnumValuesArePowerOfTwo()
        {
            SmartFlagTestEnum.FromValue(3);
        }

        [Fact]
        public void ValidResultWhenEnumFlagCombinationValuesAreExplicitlyDeclared()
        {
            var result = SmartFlagExplicitDeclaredCombinationTestEnum.FromValue(3).ToList();

            Assert.Equal("Three", result[0].Name);
        }

        [Fact]
        public void ValidResultWhenEnumFlagCombinationValuesAreExplicitlyDeclaredGivenCombinationValueGreaterThanMaximumFlagValue()
        {
            var result = SmartFlagExplicitDeclaredCombinationTestEnum.FromValue(5).ToList();

            Assert.Equal("Five", result[0].Name);
        }

        [Fact]
        public void ThrowsExceptionGivenNonImplicitCombinationValueGreaterThanMaximumFlagValue()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => SmartFlagExplicitDeclaredCombinationTestEnum.FromValue(4));
        }

        [Fact]
        public void ReturnsValidResultWhenNegativeOneAllValueIncluded()
        {
            var result = SmartFlagNegativeAndZeroMultiValueTestEnum.FromValue(3).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void ReturnsNoneEnumWhenZeroValueInput()
        {
            var result = SmartFlagNegativeAndZeroMultiValueTestEnum.FromValue(0).ToList();

            Assert.Single(result);
            Assert.Equal("None", result[0].Name);
        }

        [Fact]
        public void ReturnsAllEnumWhenNegativeOneValueInput()
        {
            var result = SmartFlagNegativeAndZeroMultiValueTestEnum.FromValue(-1).ToList();

            Assert.Single(result);
            Assert.Equal("All", result[0].Name);
        }

        [Fact]
        public void DoesNothingIfEnumValuesArePositive()
        {
            SmartFlagTestEnum.FromValue(3);
        }

        [Fact]
        public void ThrowsExceptionIfValueIsNegative()
        {
             Assert.Throws<SmartFlagEnumContainsNegativeValueException>(() => SmartFlagNegativeTestEnum.FromValue(1));
        }

        [Fact]
        public void ThrowsExceptionIfValueIsNegativeFirstValueNegative()
        {
            Assert.Throws<SmartFlagEnumContainsNegativeValueException>(() => SmartFlagNegativeTestEnum.FromValue(1));
        }

        [Fact]
        public void AlternativeValueBitshiftNotationReturnsValidValue()
        {
            var result = SmartFlagAlternativeValueNotation.FromValue(3).ToList();

            Assert.Equal(2 , result.Count);
            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
        }

        [Fact]
        public void ReturnsAllWhenGivenInputNegativeOne()
        {
            var result = SmartFlagTestEnum.FromValue(-1).ToList();

            Assert.Equal(5, result.Count);
            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
            Assert.Equal("Three", result[2].Name);
            Assert.Equal("Four", result[3].Name);
            Assert.Equal("Five", result[4].Name);
        }

        [Fact]
        public void ReturnsAllExplicitCombinationValueWhenGivenNegativeOne()
        {
            var result = SmartFlagNegativeAndZeroMultiValueTestEnum.FromValue(-1).ToList();

            Assert.Single(result);
            Assert.Equal("All", result[0].Name);
        }

        [Fact]
        public void ReturnsAllWhenGivenIntegerMaxValue()
        {
            var result = SmartFlagTestEnum.FromValue(int.MaxValue).ToList();

            Assert.Equal(5, result.Count);
            Assert.Equal("One", result[0].Name);
            Assert.Equal("Two", result[1].Name);
            Assert.Equal("Three", result[2].Name);
            Assert.Equal("Four", result[3].Name);
            Assert.Equal("Five", result[4].Name);
        }
    }
}
