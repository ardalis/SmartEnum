namespace Ardalis.SmartEnum.Dapper
{
    using System;

    /// <summary>
    /// A base type to use for creating smart enums with a backing data type of <see cref="int"/>
    /// that automatically register a Dapper type handler for themselves. The handler registered
    /// by this method maps the <em>value</em> of a <c>SmartEnum</c> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    public abstract class DapperSmartEnumByValue<TEnum> : DapperSmartEnumByValue<TEnum, int>
        where TEnum : DapperSmartEnumByValue<TEnum, int>
    {
        /// <inheritdoc/>
        protected DapperSmartEnumByValue(string name, int value) : base(name, value)
        {
        }
    }

    /// <summary>
    /// A base type to use for creating smart enums that automatically register a Dapper type
    /// handler for themselves. The handler registered by this method maps the <em>value</em> of a
    /// <c>SmartEnum</c> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    public abstract class DapperSmartEnumByValue<TEnum, TValue> : DapperSmartEnum<TEnum, TValue, SmartEnumByValueTypeHandler<TEnum, TValue>>
        where TEnum : DapperSmartEnumByValue<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        /// <inheritdoc/>
        protected DapperSmartEnumByValue(string name, TValue value) : base(name, value)
        {
        }
    }
}
