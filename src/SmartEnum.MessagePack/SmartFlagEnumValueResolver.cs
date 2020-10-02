namespace Ardalis.SmartEnum.MessagePack
{
    using System;
    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// 
    /// </summary>
    public class SmartFlagEnumValueResolver : IFormatterResolver
    {
        /// <summary>
        /// Return the instance.
        /// </summary>
        public static readonly SmartFlagEnumValueResolver Instance = new SmartFlagEnumValueResolver();

        private SmartFlagEnumValueResolver()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
                    var formatterType = typeof(SmartFlagEnumValueFormatter<,>).MakeGenericType(genericArguments);
                    Formatter = (IMessagePackFormatter<T>)Activator.CreateInstance(formatterType);
                }
            }
#pragma warning restore S3963 // "static" fields should be initialized inline
        }
    }
}