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

            AssertExceptionTypeAndMessage(exception, nameof(TestEnum));

        }

        [Theory]
        [InlineData(-2.01)]
        [InlineData(1.21)]
        [InlineData(0.01)]
        public void InvalidEnumValue_ThrowsSmartEnumDoubleNotFoundException(double invalidValue)
        {
            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(invalidValue));

            AssertExceptionTypeAndMessage(exception, nameof(TestEnumDouble));
        }

        [Fact]
        public void InvalidEnumValueWithCustomMessage_ThrowsSmartEnumNotFoundExceptionWithCustomMessage()
        {
            int invalidValue = 999;
            string customMessage = "Custom error message";

            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnum>(invalidValue, customMessage));

            AssertExceptionTypeAndMessage(exception, customMessage);
        }

        [Fact]
        public void InvalidEnumValueWithCustomMessage_ThrowsSmartEnumDoubleNotFoundExceptionWithCustomMessage()
        {
            double invalidValue = 32.1;
            string customMessage = "Custom error message";

            var exception = Assert.Throws<SmartEnumNotFoundException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(invalidValue, customMessage));

            AssertExceptionTypeAndMessage(exception, customMessage);
        }

        [Fact]
        public void InvalidEnumValueWithCustomException_ThrowsCustomException()
        {
            int invalidValue = 999;

            var customException = new InvalidOperationException("Custom exception");

            var exception = Assert.Throws<InvalidOperationException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnum>(invalidValue, exceptionCreator: () => customException));

            exception.Should().Be(customException);
        }

        [Fact]
        public void InvalidDoubleEnumValueWithCustomException_ThrowsCustomException()
        {
            double invalidValue = 32.33;

            var customException = new InvalidOperationException("Custom exception");

            var exception = Assert.Throws<InvalidOperationException>(() =>
                Guard.Against.SmartEnumOutOfRange<TestEnumDouble, double>(
                    invalidValue, 
                    exceptionCreator: () => customException));

            exception.Should().Be(customException);
        }

        private static void AssertSmartEnumIsValid<TEnum, TExpected>(TExpected expected, TEnum result)
            where TEnum : SmartEnum<TEnum, TExpected>
            where TExpected : IEquatable<TExpected>, IComparable<TExpected>
        {
            result.Should().NotBeNull();
            result.Should().BeOfType<TEnum>();
            result.Value.Should().Be(expected);
        }

        private static void AssertExceptionTypeAndMessage<T>(T exception, string message)
            where T : Exception
        {
            exception.Should().BeOfType<T>();
            exception.Message.Should().Contain(message);
        }
    }
}
