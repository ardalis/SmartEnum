namespace Ardalis.SmartEnum.Utf8Json
{
    using System;
    using global::Utf8Json;
    using global::Utf8Json.Formatters;

    public class SmartEnumValueResolver : IJsonFormatterResolver
    {
        public static readonly SmartEnumValueResolver Instance = new SmartEnumValueResolver();

        private SmartEnumValueResolver() 
        {
        }

        public IJsonFormatter<T> GetFormatter<T>() =>
            FormatterCache<T>.Formatter;

        private static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> Formatter;

            static FormatterCache()
            {
                if (typeof(T).IsSmartEnum(out var genericArguments))
                {
                    var formatterType = typeof(SmartEnumValueFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);                    
                }
            }
        }    
    }
}