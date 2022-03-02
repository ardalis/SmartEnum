namespace Ardalis.SmartEnum.Dapper
{
    using System;

    /// <summary>
    /// A base type to use for creating smart enums with a backing data type of <see cref="int"/>
    /// that automatically register a Dapper type handler for themselves. The handler registered by
    /// this method maps the <em>name</em> of a <c>SmartEnum</c> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    public abstract class DapperSmartEnumByName<TEnum> : DapperSmartEnumByName<TEnum, int>
        where TEnum : DapperSmartEnumByName<TEnum, int>
    {
        /// <inheritdoc/>
        protected DapperSmartEnumByName(string name, int value) : base(name, value)
        {
        }
    }

    /// <summary>
    /// A base type to use for creating smart enums that automatically register a Dapper type
    /// handler for themselves. The handler registered by this method maps the <em>name</em> of a
    /// <c>SmartEnum</c> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    public abstract class DapperSmartEnumByName<TEnum, TValue> : DapperSmartEnum<TEnum, TValue, SmartEnumByNameTypeHandler<TEnum, TValue>>
        where TEnum : DapperSmartEnumByName<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        /// <inheritdoc/>
        protected DapperSmartEnumByName(string name, TValue value) : base(name, value)
        {
        }
    }
}
