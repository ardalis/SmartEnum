using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.SmartEnum.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a <see cref="SmartFlagEnum{TEnum}"/> does not contain consecutive power of two values.
    /// </summary>
    [Serializable]
    public class SmartFlagEnumDoesNotContainPowerOfTwoValuesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumDoesNotContainPowerOfTwoValuesException"/> class.
        /// </summary>
        public SmartFlagEnumDoesNotContainPowerOfTwoValuesException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumDoesNotContainPowerOfTwoValuesException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected SmartFlagEnumDoesNotContainPowerOfTwoValuesException(
            System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumDoesNotContainPowerOfTwoValuesException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SmartFlagEnumDoesNotContainPowerOfTwoValuesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFlagEnumDoesNotContainPowerOfTwoValuesException"/> class with a specified error message and 
        /// a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not a null reference, 
        /// the current exception is raised in a <c>catch</c> block that handles the inner exception.
        /// </param>
        public SmartFlagEnumDoesNotContainPowerOfTwoValuesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
