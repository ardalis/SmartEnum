using System;

namespace Ardalis.SmartEnum.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InvalidFlagEnumValueParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFlagEnumValueParseException"/> class.
        /// </summary>
        public InvalidFlagEnumValueParseException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFlagEnumValueParseException"/> class with a user specified error <paramref name="message"/>. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidFlagEnumValueParseException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFlagEnumValueParseException"/> class with a user specified error <paramref name="message"/>
        /// and a wrapped <paramref name="innerException"/> that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException"></param> The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not a null reference, 
        /// the current exception is raised in a <c>catch</c> block that handles the inner exception.
        public InvalidFlagEnumValueParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
