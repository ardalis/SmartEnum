namespace Ardalis.SmartEnum.Dapper
{
    using global::Dapper;
    using System;

    /// <summary>
    /// A static class responsible for ensuring that the SmartEnum type handler specified by
    /// <typeparamref name="TSmartEnumTypeHandler"/> has been added to <see cref="SqlMapper"/>
    /// for the SmartEnum type specified by <typeparamref name="TEnum"/>, which has a backing data
    /// type of <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TSmartEnumTypeHandler">
    /// The type of <see cref="SmartEnumTypeHandler{TEnum, TValue}"/> to register for a given
    /// <c>SmartEnum</c> type.
    /// </typeparam>
    public static class DapperRegistration<TEnum, TSmartEnumTypeHandler>
        where TEnum : SmartEnum<TEnum, int>
        where TSmartEnumTypeHandler : SmartEnumTypeHandler<TEnum, int>, new()
    {
        /// <summary>
        /// Ensures that the SmartEnum type handler specified by
        /// <typeparamref name="TSmartEnumTypeHandler"/> has been added to <see cref="SqlMapper"/>
        /// for the SmartEnum type specified by <typeparamref name="TEnum"/>.
        /// <para>Note: Calling this method more than once with a given set of generic arguments
        /// has no effect.</para>
        /// </summary>
        public static void EnsureTypeHandlerAdded()
        {
            DapperRegistration<TEnum, int, TSmartEnumTypeHandler>.EnsureTypeHandlerAdded();
        }
    }

    /// <summary>
    /// A static class responsible for ensuring that the SmartEnum type handler specified by
    /// <typeparamref name="TSmartEnumTypeHandler"/> has been added to <see cref="SqlMapper"/>
    /// for the SmartEnum type specified by <typeparamref name="TEnum"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TSmartEnumTypeHandler">
    /// The type of <see cref="SmartEnumTypeHandler{TEnum, TValue}"/> to register for a given
    /// <c>SmartEnum</c> type.
    /// </typeparam>
    public static class DapperRegistration<TEnum, TValue, TSmartEnumTypeHandler>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
        where TSmartEnumTypeHandler : SmartEnumTypeHandler<TEnum, TValue>, new()
    {
        static DapperRegistration()
        {
            var typeHandler = new TSmartEnumTypeHandler();

            foreach (var customAttribute in Attribute.GetCustomAttributes(typeof(TEnum), false))
                typeHandler.ConfigureFromCustomAttribute(customAttribute);

            SqlMapper.AddTypeHandler(typeof(TEnum), typeHandler);
        }

        /// <summary>
        /// Ensures that the SmartEnum type handler specified by
        /// <typeparamref name="TSmartEnumTypeHandler"/> has been added to <see cref="SqlMapper"/>
        /// for the SmartEnum type specified by <typeparamref name="TEnum"/>.
        /// <para>Note: Calling this method more than once with a given set of generic arguments
        /// has no effect.</para>
        /// </summary>
        public static void EnsureTypeHandlerAdded()
        {
            // Do nothing. Calling this method ensures that the static constructor (which *does* do
            // something) is called.
        }
    }
}
