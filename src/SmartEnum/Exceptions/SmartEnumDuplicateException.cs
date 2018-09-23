using System;

namespace SmartEnum.Exceptions
{
    public class SmartEnumDuplicateException : Exception
    {
        public Type Type { get; }
        public object Duplicate { get; }

        public SmartEnumDuplicateException(Type type, object duplicate) : 
            base($"'{duplicate} is a duplicate in '{type.Name}'")
        {
            Type = type;
            Duplicate = duplicate;
        }

        protected SmartEnumDuplicateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public SmartEnumDuplicateException(Type type, object duplicate, string message) : base(message)
        {
            Type = type;
            Duplicate = duplicate;
        }

        public SmartEnumDuplicateException(Type type, object duplicate, string message, Exception innerException) : base(message, innerException)
        {
            Type = type;
            Duplicate = duplicate;
        }
    }
}
