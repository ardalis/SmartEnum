using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.SmartEnum.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a negative value is provided as an argument value.
    /// </summary>
    [Serializable]
    public class NegativeValueArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeValueArgumentException"/> class.
        /// </summary>
        public NegativeValueArgumentException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeValueArgumentException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected NegativeValueArgumentException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeValueArgumentException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NegativeValueArgumentException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeValueArgumentException"/> class with a specified error message and 
        /// a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not a null reference, 
        /// the current exception is raised in a <c>catch</c> block that handles the inner exception.
        /// </param>
        public NegativeValueArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
