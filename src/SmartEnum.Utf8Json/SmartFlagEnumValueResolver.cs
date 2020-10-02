namespace Ardalis.SmartEnum.Utf8Json
{
    using System;
    using global::Utf8Json;
    using global::Utf8Json.Formatters;

    /// <summary>
    /// 
    /// </summary>
    public class SmartFlagEnumValueResolver : IJsonFormatterResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly SmartFlagEnumValueResolver Instance = new SmartFlagEnumValueResolver();

        public SmartFlagEnumValueResolver()
        {
        }

        /// <summary>
        /// 
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
                if (typeof(T).IsSmartFlagEnum(out var genericArguments))
                {
                    var formatterType = typeof(SmartFlagEnumValueFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);
                }
            }
#pragma warning restore S3963 // "static" fields should be initialized inline
        }
    }
}