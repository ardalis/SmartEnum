namespace Ardalis.SmartEnum.Dapper
{
    using System;
    using System.Data;
    using static global::Dapper.SqlMapper;

    /// <summary>
    /// The base class for <c>SmartEnum</c> type handlers.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    public abstract class SmartEnumTypeHandler<TEnum, TValue> : TypeHandler<TEnum>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Gets or sets the <see cref="System.Data.DbType"/> to be assigned to a parameter before
        /// a command executes. If <see langword="null"/>, a parameter will not have its
        /// <see cref="IDataParameter.DbType"/> property set.
        /// </summary>
        public DbType? DbType { get; set; }

        /// <inheritdoc/>
        public override void SetValue(IDbDataParameter parameter, TEnum value)
        {
            object parsed = null;
            if (value != null)
                parsed = GetParameterValue(value);

            parameter.Value = parsed ?? DBNull.Value;

            if (DbType.HasValue)
                parameter.DbType = DbType.Value;
        }

        /// <summary>
        /// When overriden in a derived class, gets the value to be assigned to a parameter before
        /// a command executes.
        /// <para>Called from the base class's <see cref="SetValue(IDbDataParameter, TEnum)"/>
        /// method.</para>
        /// </summary>
        /// <param name="smartEnum">
        /// The SmartEnum to be used to get the parameter value.
        /// <para>When called from the base <see cref="SmartEnumTypeHandler{TEnum, TValue}"/>
        /// class, this parameter will never be <see langword="null"/>.</para>
        /// </param>
        protected abstract object GetParameterValue(TEnum smartEnum);

        /// <summary>
        /// Configures the type handler from the specified attribute.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the type handler was modified, otherwise
        /// <see langword="false"/>.
        /// </returns>
        public virtual bool ConfigureFromCustomAttribute(Attribute customAttribute)
        {
            if (customAttribute is DoNotSetDbTypeAttribute)
            {
                DbType = null;
                return true;
            }

            if (customAttribute is DbTypeAttribute dbTypeAttribute)
            {
                DbType = dbTypeAttribute.DbType;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets exception to be thrown when a database value has an unexpected type.
        /// </summary>
        protected static ArgumentException UnexpectedDatabaseValue(object value, Type expectedType, Exception innerException = null) =>
            new ArgumentException($"Expected database value to be of type {expectedType}, but was {value.GetType()}.", nameof(value), innerException);
    }
}
