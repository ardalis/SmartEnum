namespace Ardalis.SmartEnum.Utf8Json
{
    using System;
    using global::Utf8Json;

    /// <summary>
    /// 
    /// </summary>
    public class SmartEnumValueResolver : IJsonFormatterResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly SmartEnumValueResolver Instance = new SmartEnumValueResolver();

        private SmartEnumValueResolver()
        {
        }

        /// <summary>
        /// GetFormatter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
                    var formatterType = typeof(SmartEnumValueFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);
                }
            }
#pragma warning restore S3963 // "static" fields should be initialized inline
        }
    }
}