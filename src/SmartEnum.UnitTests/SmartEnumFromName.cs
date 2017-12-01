using Ardalis.SmartEnum;
using SmartEnum.Exceptions;
using System;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumFromName
    {
        [Fact]
        public void ReturnsEnumGivenMatchingName()
        {
            Assert.Equal(TestEnum.One, TestEnum.FromName("One"));
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => TestEnum.FromName(String.Empty));
        }

        [Fact]
        public void ThrowsGivenNull()
        {
            Assert.Throws<ArgumentNullException>(() => TestEnum.FromName(null));
        }

        [Fact]
        public void ThrowsGivenNonMatchingString()
        {
            Assert.Throws<SmartEnumNotFoundException>(() => TestEnum.FromName("Doesn't Exist"));
        }
    }
}
