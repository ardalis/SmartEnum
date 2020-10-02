using global::MessagePack;
using global::MessagePack.Formatters;
using System;

namespace Ardalis.SmartEnum.MessagePack
{
    public class SmartFlagEnumNameResolver : IFormatterResolver
    {
        public static readonly SmartFlagEnumNameResolver Instance = new SmartFlagEnumNameResolver();

        private SmartFlagEnumNameResolver()
        {
        }

        public IMessagePackFormatter<T> GetFormatter<T>() =>
            FormatterCache<T>.Formatter;

        private static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> Formatter;

#pragma warning disable S3963 // "static" fields should be initialized inline
            static FormatterCache()
            {
                if (typeof(T).IsSmartFlagEnum(out var genericArguments))
                {
                    var formatterType = typeof(SmartFlagEnumNameFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IMessagePackFormatter<T>)Activator.CreateInstance(formatterType);
                }
            }
#pragma warning restore S3963 // "static" fields should be initialized inline
        }
    }
}
