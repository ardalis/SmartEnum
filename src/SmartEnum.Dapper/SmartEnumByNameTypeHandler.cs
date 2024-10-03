namespace Ardalis.SmartEnum.Dapper
{
    using System;

    /// <summary>
    /// A type handler that maps the <em>name</em> of a <c>SmartEnum</c> with a backing data type
    /// of <see cref="int"/> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    public class SmartEnumByNameTypeHandler<TEnum> : SmartEnumByNameTypeHandler<TEnum, int>
        where TEnum : SmartEnum<TEnum, int>
    {
    }

    /// <summary>
    /// A type handler that maps the <em>name</em> of a <c>SmartEnum</c> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    public class SmartEnumByNameTypeHandler<TEnum, TValue> : SmartEnumTypeHandler<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEnumByNameTypeHandler{TEnum, TValue}"/> class.
        /// </summary>
        public SmartEnumByNameTypeHandler()
        {
            DbType = System.Data.DbType.String;
        }

        /// <summary>
        /// Gets or sets whether to ignore case when creating the SmartEnum from name.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <inheritdoc/>
        public override TEnum Parse(object value)
        {
            if (value is null || value is DBNull)
                return null;

            if (value is string stringValue)
                return SmartEnum<TEnum, TValue>.FromName(stringValue, IgnoreCase);

            throw UnexpectedDatabaseValue(value, typeof(string));
        }

        /// <inheritdoc/>
        protected override object GetParameterValue(TEnum smartEnum)
        {
            return smartEnum.Name;
        }

        /// <inheritdoc/>
        public override bool ConfigureFromCustomAttribute(Attribute customAttribute)
        {
            if (base.ConfigureFromCustomAttribute(customAttribute))
                return true;

            if (customAttribute is IgnoreCaseAttribute)
            {
                IgnoreCase = true;
                return true;
            }

            return false;
        }
    }
}
