using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SmartEnum.UnitTests
{
    /// <summary>
    /// A helper class to aid in testing the WCF Serialization of an object.
    /// Source: https://stackoverflow.com/a/974035 with the addition of adding
    /// support for JSON by adding methods using the <see cref="DataContractJsonSerializer"/>
    /// instead of the regular <see cref="DataContractSerializer"/>.
    /// </summary>
    public static class WcfTestHelper
    {
        /// <summary>
        /// Uses a <see cref="DataContractSerializer"/> to serialise the object into
        /// memory, then deserialise it again and return the result.  This is useful
        /// in tests to validate that your object is serialisable, and that it
        /// serialises correctly.
        /// </summary>
        public static T DataContractSerializationRoundTrip<T>(T obj)
        {
            return DataContractSerializationRoundTrip(obj, null);
        }

        /// <summary>
        /// Uses a <see cref="DataContractSerializer"/> to serialise the object into
        /// memory, then deserialise it again and return the result.  This is useful
        /// in tests to validate that your object is serialisable, and that it
        /// serialises correctly.
        /// </summary>
        public static T DataContractSerializationRoundTrip<T>(T obj,
            IEnumerable<Type> knownTypes)
        {
            var serializer = new DataContractSerializer(obj.GetType(), knownTypes);
            var memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;
            obj = (T)serializer.ReadObject(memoryStream);
            return obj;
        }

        /// <summary>
        /// Uses a <see cref="DataContractJsonSerializer"/> to serialise the object into
        /// memory, then deserialise it again and return the result.  This is useful
        /// in tests to validate that your object is serialisable, and that it
        /// serialises correctly.
        /// </summary>
        public static T DataContractJsonSerializationRoundTrip<T>(T obj)
        {
            return DataContractJsonSerializationRoundTrip(obj, null);
        }

        /// <summary>
        /// Uses a <see cref="DataContractJsonSerializer"/> to serialise the object into
        /// memory, then deserialise it again and return the result.  This is useful
        /// in tests to validate that your object is serialisable, and that it
        /// serialises correctly.
        /// </summary>
        public static T DataContractJsonSerializationRoundTrip<T>(T obj,
            IEnumerable<Type> knownTypes)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType(), knownTypes);
            var memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;
            obj = (T)serializer.ReadObject(memoryStream);
            return obj;
        }
    }
}