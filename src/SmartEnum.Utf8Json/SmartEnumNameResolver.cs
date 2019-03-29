namespace Ardalis.SmartEnum.Utf8Json
{
    using System;
    using global::Utf8Json;
    using global::Utf8Json.Formatters;

    public class SmartEnumNameResolver : IJsonFormatterResolver
    {
        public static readonly SmartEnumNameResolver Instance = new SmartEnumNameResolver();

        private SmartEnumNameResolver() 
        {
        }

        public IJsonFormatter<T> GetFormatter<T>() =>
            FormatterCache<T>.Formatter;

        private static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> Formatter;

#pragma warning disable S3963 // "static" fields should be initialized inline
            static FormatterCache()
            {
                if (typeof(T).IsSmartEnum(out var genericArguments))
                {
                    var formatterType = typeof(SmartEnumNameFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);                    
                }
            }
#pragma warning restore S3963 // "static" fields should be initialized inline
        }    
    }
}