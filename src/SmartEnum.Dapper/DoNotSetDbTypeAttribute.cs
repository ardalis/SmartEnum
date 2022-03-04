namespace Ardalis.SmartEnum.Dapper
{
    using System;
    using System.Data;

    /// <summary>
    /// Indicates that a <see cref="SmartEnumTypeHandler{TEnum, TValue}"/> should <em>not</em> set
    /// the <see cref="IDataParameter.DbType"/> property of a parameter before a command executes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DoNotSetDbTypeAttribute : Attribute
    {
    }
}
