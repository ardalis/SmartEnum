using SmartEnum.Exceptions;
using System;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumFromName
    {
        [Fact]
        public void ReturnsEnumGivenNoExplicitPriorUse()
        {
            string expected = "One";
            Assert.Equal(expected, TestEnum.FromName(expected).Name);
        }

        [Fact]
        public void ReturnsEnumGivenExplicitPriorUse()
        {
            string expected = TestEnum.One.Name;
            Assert.Equal(expected, TestEnum.FromName(expected).Name);
        }

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

        [Fact]
        public void ThrowsWithExpectedMessageGivenNonMatchingString()
        {
            string name = "Doesn't Exist";
            string expected = $"No TestEnum with Name \"{name}\" found.";
            string actual = "";

            try
            {
                var testEnum = TestEnum.FromName(name);
            }
            catch (SmartEnumNotFoundException ex)
            {
                actual = ex.Message;
            }

            Assert.Equal(expected, actual);
        }
    }
}
