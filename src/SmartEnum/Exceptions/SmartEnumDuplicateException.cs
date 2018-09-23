using System;

namespace SmartEnum.Exceptions
{
    public class SmartEnumDuplicateException : Exception
    {
        public SmartEnumDuplicateException() : base()
        {
        }

        protected SmartEnumDuplicateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public SmartEnumDuplicateException(string message) : base(message)
        {
        }

        public SmartEnumDuplicateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
