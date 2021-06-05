namespace Ardalis.SmartEnum.MessagePack
{
    using System;
    using global::MessagePack;
    using global::MessagePack.Formatters;

    public class SmartEnumNameResolver : IFormatterResolver
    {
        public static readonly SmartEnumNameResolver Instance = new SmartEnumNameResolver();

        private SmartEnumNameResolver()
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
                if (typeof(T).IsSmartEnum(out var genericArguments))
                {
                    var formatterType = typeof(SmartEnumNameFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IMessagePackFormatter<T>)Activator.CreateInstance(formatterType);
                }
            }
#pragma warning restore S3963 // "static" fields should be initialized inline
        }    
    }
}