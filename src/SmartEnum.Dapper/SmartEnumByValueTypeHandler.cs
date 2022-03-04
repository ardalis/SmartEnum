namespace Ardalis.SmartEnum.Dapper
{
    using System;

    /// <summary>
    /// A type handler that maps the <em>value</em> of a <c>SmartEnum</c> with a backing data type
    /// of <see cref="int"/> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    public class SmartEnumByValueTypeHandler<TEnum> : SmartEnumByValueTypeHandler<TEnum, int>
        where TEnum : SmartEnum<TEnum, int>
    {
    }

    /// <summary>
    /// A type handler that maps the <em>value</em> of a <c>SmartEnum</c> to a database column.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <c>SmartEnum</c>.</typeparam>
    /// <typeparam name="TValue">The backing data type of the <c>SmartEnum</c>.</typeparam>
    public class SmartEnumByValueTypeHandler<TEnum, TValue> : SmartEnumTypeHandler<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEnumByValueTypeHandler{TEnum, TValue}"/>
        /// class, setting the <see cref="SmartEnumTypeHandler{TEnum, TValue}.DbType"/> property
        /// according to the type of the <typeparamref name="TValue"/> type parameter.
        /// </summary>
        public SmartEnumByValueTypeHandler()
        {
            DbType = GetDbTypeFromTValue();
        }

        /// <inheritdoc/>
        public override TEnum Parse(object value)
        {
            if (value == null || value is DBNull)
                return null;

            if (value is not TValue tValue)
            {
                try
                {
                    tValue = (TValue)Convert.ChangeType(value, typeof(TValue));
                }
                catch (Exception ex)
                {
                    throw UnexpectedDatabaseValue(value, typeof(TValue), ex);
                }
            }

            return SmartEnum<TEnum, TValue>.FromValue(tValue);
        }

        /// <inheritdoc/>
        protected override object GetParameterValue(TEnum smartEnum)
        {
            return smartEnum.Value;
        }

        private static System.Data.DbType? GetDbTypeFromTValue()
        {
            var tValueType = typeof(TValue);

            if (tValueType == typeof(bool)) return System.Data.DbType.Boolean;
            if (tValueType == typeof(byte)) return System.Data.DbType.Byte;
            if (tValueType == typeof(char)) return System.Data.DbType.StringFixedLength;
            if (tValueType == typeof(DateTime)) return System.Data.DbType.DateTime;
            if (tValueType == typeof(DateTimeOffset)) return System.Data.DbType.DateTimeOffset;
            if (tValueType == typeof(decimal)) return System.Data.DbType.Decimal;
            if (tValueType == typeof(double)) return System.Data.DbType.Double;
            if (tValueType == typeof(Guid)) return System.Data.DbType.Guid;
            if (tValueType == typeof(short)) return System.Data.DbType.Int16;
            if (tValueType == typeof(int)) return System.Data.DbType.Int32;
            if (tValueType == typeof(long)) return System.Data.DbType.Int64;
            if (tValueType == typeof(sbyte)) return System.Data.DbType.SByte;
            if (tValueType == typeof(float)) return System.Data.DbType.Single;
            if (tValueType == typeof(string)) return System.Data.DbType.String;
            if (tValueType == typeof(ushort)) return System.Data.DbType.UInt16;
            if (tValueType == typeof(uint)) return System.Data.DbType.UInt32;
            if (tValueType == typeof(ulong)) return System.Data.DbType.UInt64;

            return null;
        }
    }
}
