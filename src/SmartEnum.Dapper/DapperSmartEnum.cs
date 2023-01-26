namespace Ardalis.SmartEnum.Dapper
{
    //using global::Dapper;
    using System;

    /// <summary>
    /// A base type to use for creating smart enums with a backing data type of <see cref="int"/>
    /// that automatically register a Dapper type handler for themselves.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TSmartEnumTypeHandler">
    /// The type of <see cref="SmartEnumTypeHandler{TEnum, TValue}"/> to register for a given
    /// <c>SmartEnum</c> type.
    /// </typeparam>
    public abstract class DapperSmartEnum<TEnum, TSmartEnumTypeHandler> : DapperSmartEnum<TEnum, int, TSmartEnumTypeHandler>
        where TEnum : DapperSmartEnum<TEnum, int, TSmartEnumTypeHandler>
        where TSmartEnumTypeHandler : SmartEnumTypeHandler<TEnum, int>, new()
    {
        /// <inheritdoc/>
        protected DapperSmartEnum(string name, int value) : base(name, value)
        {
        }
    }

    /// <summary>
    /// A base type to use for creating smart enums that automatically register a Dapper type
    /// handler for themselves.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TSmartEnumTypeHandler">
    /// The type of <see cref="SmartEnumTypeHandler{TEnum, TValue}"/> to register for a given
    /// <c>SmartEnum</c> type.
    /// </typeparam>
    public abstract class DapperSmartEnum<TEnum, TValue, TSmartEnumTypeHandler> : SmartEnum<TEnum, TValue>
        where TEnum : DapperSmartEnum<TEnum, TValue, TSmartEnumTypeHandler>
        where TValue : IEquatable<TValue>, IComparable<TValue>
        where TSmartEnumTypeHandler : SmartEnumTypeHandler<TEnum, TValue>, new()
    {
        static DapperSmartEnum()
        {
            DapperRegistration<TEnum, TValue, TSmartEnumTypeHandler>.EnsureTypeHandlerAdded();
        }

        /// <inheritdoc/>
        protected DapperSmartEnum(string name, TValue value) : base(name, value)
        {
        }
    }
}
