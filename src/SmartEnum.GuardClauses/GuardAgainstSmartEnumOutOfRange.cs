using Ardalis.GuardClauses;
using System;

namespace Ardalis.SmartEnum.GuardClauses
{
    /// <summary>
    /// Provides guard clauses to ensure input values are valid instances of a specified SmartEnum.
    /// </summary>
    public static class GuardAgainstSmartEnumOutOfRange
    {
        /// <summary>
        /// Throws a<see cref="SmartEnumNotFoundException" /> or a custom<see cref="Exception" />
        /// if <paramref name="input"/> is not a valid <see cref="SmartEnum{TEnum}"/> value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the smart enum.</typeparam>
        /// <param name="guardClause">The guard clause interface.</param>
        /// <param name="input">The value to check against the smart enum values.</param>
        /// <param name="message">Optional. Custom error message to pass to <see cref="SmartEnumNotFoundException"/>.</param>
        /// <param name="exceptionCreator">Optional. A function that creates a custom exception.</param>
        /// <returns>The valid  <see cref="SmartEnum{TEnum}"/> value <paramref name="input" />.</returns>
        /// <exception cref="SmartEnumNotFoundException">Thrown when <paramref name="input" /> 
        /// is not a valid enum value, and no custom exception is provided.</exception>
        /// <exception cref="Exception">Thrown when a custom exception is provided by <paramref name="exceptionCreator" />.</exception>
        public static TEnum SmartEnumOutOfRange<TEnum>(
            this IGuardClause guardClause,
            int input,
            string message = null,
            Func<Exception> exceptionCreator = null) 
            where TEnum : SmartEnum<TEnum>
        {
            return guardClause.SmartEnumOutOfRange<TEnum, int>(input, message, exceptionCreator);
        }

        /// <summary>
        /// Throws a <see cref="SmartEnumNotFoundException"/> or a custom <see cref="Exception"/>
        /// if <paramref name="input"/> is not a valid <see cref="SmartEnum{TEnum, TValue}"/> value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the smart enum.</typeparam>
        /// <typeparam name="TValue">The type of the value that the smart enum uses.</typeparam>
        /// <param name="guardClause">The guard clause interface.</param>
        /// <param name="input">The value to check against the smart enum values.</param>
        /// <param name="message">Optional. Custom error message to pass to <see cref="SmartEnumNotFoundException"/>.</param>
        /// <param name="exceptionCreator">Optional. A function that creates a custom exception.</param>
        /// <returns>The valid enum value <typeparamref name="TEnum"/> corresponding to <paramref name="input"/>.</returns>
        /// <exception cref="SmartEnumNotFoundException">Thrown when <paramref name="input"/> 
        /// is not a valid enum value and no custom exception is provided.</exception>
        /// <exception cref="Exception">Thrown when a custom exception 
        /// is provided by <paramref name="exceptionCreator"/>.</exception>
        public static TEnum SmartEnumOutOfRange<TEnum, TValue>(
            this IGuardClause guardClause,
            TValue input,
            string message = null,
            Func<Exception> exceptionCreator = null)
            where TEnum : SmartEnum<TEnum, TValue>
            where TValue : IEquatable<TValue>, IComparable<TValue>
        {
            if (SmartEnum<TEnum, TValue>.TryFromValue(input, out TEnum result))
            {
                return result;
            }

            var exceptionMessage = message ?? $"The value '{input}' is not a valid {typeof(TEnum).Name}.";
            throw exceptionCreator?.Invoke() ?? new SmartEnumNotFoundException(exceptionMessage);
        }
    }
}
