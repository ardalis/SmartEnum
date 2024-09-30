namespace Ardalis.SmartEnum
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The exception that is thrown when a item is not found.
    /// </summary>
    [Serializable]
    [SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>")]
    public class SmartEnumNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEnumNotFoundException"/> class.
        /// </summary>
        public SmartEnumNotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEnumNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SmartEnumNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEnumNotFoundException"/> class with a specified error message and
        /// a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not a null reference,
        /// the current exception is raised in a <c>catch</c> block that handles the inner exception.
        /// </param>
        public SmartEnumNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
