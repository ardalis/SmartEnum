namespace Ardalis.SmartEnum.Dapper
{
    using System;
    using System.Data;

    /// <summary>
    /// Indicates that a <see cref="SmartEnumTypeHandler{TEnum, TValue}"/> should assign the
    /// specified <see cref="System.Data.DbType"/> to a parameter before a command executes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DbTypeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbTypeAttribute"/> class with the
        /// specified <see cref="System.Data.DbType"/> value.
        /// </summary>
        /// <param name="value"></param>
        public DbTypeAttribute(DbType value)
        {
            DbType = value;
        }

        /// <summary>
        /// Gets the <see cref="System.Data.DbType"/> to be assigned to a parameter before a
        /// command executes.
        /// </summary>
        public DbType DbType { get; }
    }
}
