using System.Collections.Generic;

namespace Ardalis.SmartEnum.ProtoBufNet
{
    using System;
    using System.IO;
    using ProtoBuf.Meta;

    static class Utils
    {
        public static T DeepClone<T>(T value, TypeModel model)
        {
            using(var stream = new MemoryStream())
            {
                model.Serialize(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)model.Deserialize(stream, null, typeof(T));
            }        
        }
    }
}