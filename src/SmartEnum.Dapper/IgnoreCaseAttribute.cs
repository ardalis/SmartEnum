namespace Ardalis.SmartEnum.Dapper
{
    using System;

    /// <summary>
    /// Indicates that a <see cref="SmartEnumByNameTypeHandler{TEnum, TValue}"/> should ignore
    /// case when parsing a database value into a <c>SmartEnum</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreCaseAttribute : Attribute
    {
    }
}
