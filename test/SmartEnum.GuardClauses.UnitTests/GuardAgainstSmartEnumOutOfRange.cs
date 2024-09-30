using Ardalis.GuardClauses;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.GuardClauses;
using FluentAssertions;
using System;
using Xunit;

namespace SmartEnum.GuardClauses.UnitTests
{
    public class GuardAgainstSmartEnumOutOfRange
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ValidSmartEnumValues_ReturnsSmartEnum(int input)
        {
            var result = Guard.Against.SmartEnumOutOfRange<TestEnum>(input);
            
            AssertSmartEnumIsValid(input, result);
        }

        [Theory]
        [InlineData(1.2)]
        [InlineData(2.3)]
        [InlineData(3.4)]
        public void ValidSmartEnumValues_ReturnsSmartEnumDouble(double input)
        {
            TestEnumDouble result = Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(input);
            
            AssertSmartEnumIsValid(input, result);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(-1)]
        [InlineData(0)]
        public void InvalidEnumValue_ThrowsSmartEnumNotFoundException(int invalidValue)
        {
            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnum>(invalidValue));

            AssertException<SmartEnumNotFoundException>(exception, nameof(TestEnum));
        }

        [Theory]
        [InlineData(-2.01)]
        [InlineData(1.21)]
        [InlineData(0.01)]
        public void InvalidEnumValue_ThrowsSmartEnumDoubleNotFoundException(double invalidValue)
        {
            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(invalidValue));

            AssertException<SmartEnumNotFoundException>(exception, nameof(TestEnumDouble));
        }

        [Fact]
        public void InvalidEnumValueWithCustomMessage_ThrowsSmartEnumNotFoundExceptionWithCustomMessage()
        {
            const int invalidValue = 999;
            string customMessage = "Custom error message";

            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnum>(invalidValue, customMessage));

            AssertException<SmartEnumNotFoundException>(exception, customMessage);
        }

        [Fact]
        public void InvalidEnumValueWithCustomMessage_ThrowsSmartEnumDoubleNotFoundExceptionWithCustomMessage()
        {
            const double invalidValue = 32.1;
            string customMessage = "Custom error message";

            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(invalidValue, customMessage));

            AssertException<SmartEnumNotFoundException>(exception, customMessage);
        }

        [Fact]
        public void InvalidEnumValueWithCustomException_ThrowsCustomException()
        {
            const int invalidValue = 999;
            const string Message = "Custom exception";

            var customException = new InvalidOperationException(Message);

            var exception = Assert.Throws<InvalidOperationException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnum>(
                    invalidValue,
                    message: "ignored",
                    exceptionCreator: () => customException));

            AssertException<InvalidOperationException>(exception, Message);
        }

        [Fact]
        public void InvalidDoubleEnumValueWithCustomException_ThrowsCustomException()
        {
            const double invalidValue = 32.33;
            const string Message = "Custom exception";

            var customException = new InvalidOperationException(Message);

            var exception = Assert.Throws<InvalidOperationException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(
                    invalidValue,
                    message: "ignored",
                    exceptionCreator: () => customException));

            AssertException<InvalidOperationException>(exception, Message);
        }

        private static void AssertSmartEnumIsValid<TEnum, TExpected>(TExpected expected, TEnum result)
            where TEnum : SmartEnum<TEnum, TExpected>
            where TExpected : IEquatable<TExpected>, IComparable<TExpected>
        {
            result.Should().NotBeNull();
            result.Should().BeOfType<TEnum>();
            result.Value.Should().Be(expected);
        }

        private static void AssertException<TException>(Exception exception, string errorMsg)
            where TException : Exception
        {
            exception.Should().BeOfType<TException>();
            exception.Message.Should().Contain(errorMsg);
        }
    }
}
