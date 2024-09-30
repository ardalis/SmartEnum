using System;
using System.Diagnostics.CodeAnalysis;

namespace Ardalis.SmartEnum.Exceptions
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    [SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>")]
    public class SmartFlagEnumContainsNegativeValueException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumContainsNegativeValueException"/> class.
        /// </summary>
        public SmartFlagEnumContainsNegativeValueException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumContainsNegativeValueException"/> class with a user specified error <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SmartFlagEnumContainsNegativeValueException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumContainsNegativeValueException"/> class with a user specified error <paramref name="message"/>
        /// and a wrapped <paramref name="innerException"/> that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException"></param> The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not a null reference,
        /// the current exception is raised in a <c>catch</c> block that handles the inner exception.
        public SmartFlagEnumContainsNegativeValueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
