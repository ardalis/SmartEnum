using System;

namespace SmartEnum.Exceptions
{
    public class SmartEnumNotFoundException : Exception
    {
        public SmartEnumNotFoundException() : base()
        {
        }

        protected SmartEnumNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public SmartEnumNotFoundException(string message) : base(message)
        {
        }

        public SmartEnumNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
