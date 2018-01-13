﻿using SmartEnum.Exceptions;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumFromValue
    {
        [Fact]
        public void ReturnsEnumGivenMatchingValue()
        {
            Assert.Equal(TestEnum.One, TestEnum.FromValue(1));
        }

        [Fact]
        public void ThrowsGivenNonMatchingValue()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => TestEnum.FromValue(-1));
        }

        [Fact]
        public void ThrowsWithExpectedMessageGivenNonMatchingValue()
        {
            string expected = $"No TestEnum with Value -1 found.";
            string actual = "";

            try
            {
                var testEnum = TestEnum.FromValue(-1);
            }
            catch (SmartEnumNotFoundException ex)
            {
                actual = ex.Message;
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnsDefaultEnumGivenNonMatchingValue()
        {
            var defaultEnum = TestEnum.One;

            Assert.Equal(defaultEnum, TestEnum.FromValue(-1, defaultEnum));
        }
    }
}