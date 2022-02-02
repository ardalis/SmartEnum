using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Ardalis.SmartEnum.Core;
using Ardalis.SmartEnum.Exceptions;

namespace Ardalis.SmartEnum
{
    /// <summary>
    /// A base type to use for creating smart enums with inner value of type <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type that is inheriting from this class</typeparam>.
    /// <remarks></remarks>
    public abstract class SmartFlagEnum<TEnum> :
        SmartFlagEnum<TEnum, int>
        where TEnum : SmartFlagEnum<TEnum, int>
    {
        protected SmartFlagEnum(string name, int value) :
            base(name, value)
        {
        }
    }

    /// <summary>
    /// A base type to use for creating smart enums.
    /// </summary>
    /// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
    /// <typeparam name="TValue">The type of the inner value.</typeparam>
    /// <remarks></remarks>
    public abstract class SmartFlagEnum<TEnum, TValue> :
        SmartFlagEngine<TEnum, TValue>,
        ISmartEnum,
        IEquatable<SmartFlagEnum<TEnum, TValue>>,
        IComparable<SmartFlagEnum<TEnum, TValue>>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        static readonly Lazy<Dictionary<string, TEnum>> _fromName =
            new Lazy<Dictionary<string, TEnum>>(() => GetAllOptions().ToDictionary(item => item.Name));

        static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase =
            new Lazy<Dictionary<string, TEnum>>(() => GetAllOptions().ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

        private static IEnumerable<TEnum> GetAllOptions()
        {
            Type baseType = typeof(TEnum);
            IEnumerable<Type> enumTypes = Assembly.GetAssembly(baseType).GetTypes().Where(t => baseType.IsAssignableFrom(t));

            List<TEnum> options = new List<TEnum>();
            foreach (Type enumType in enumTypes)
            {
                List<TEnum> typeEnumOptions = enumType.GetFieldsOfType<TEnum>();
                options.AddRange(typeEnumOptions);
            }

            return options.OrderBy(t => t.Value);
        }

        /// <summary>
        /// Gets a collection containing all the instances of <see cref="SmartFlagEnum{TEnum, TValue}"/>.
        /// </summary>
        /// <value>A <see cref="IReadOnlyCollection{TEnum}"/> containing all the instances of <see cref="SmartFlagEnum{TEnum, TValue}"/>.</value>
        /// <remarks>Retrieves all the instances of <see cref="SmartFlagEnum{TEnum, TValue}"/> referenced by public static read-only fields in the current class or its bases.</remarks>
        public static IReadOnlyCollection<TEnum> List =>
            _fromName.Value.Values
                .ToList()
                .AsReadOnly();

        private readonly string _name;
        private readonly TValue _value;

        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <value>A <see cref="String"/> that is the names of the <see cref="SmartFlagEnum{TEnum, TValue}"/>.</value>
        public string Name =>
            _name;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>A <typeparamref names="TValue"/> that is the value of the <see cref="SmartFlagEnum{TEnum, TValue}"/>.</value>
        public TValue Value =>
            _value;

        protected SmartFlagEnum(string name, TValue value)
        {
            if (String.IsNullOrEmpty(name))
                ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));
            if (value == null)
                ThrowHelper.ThrowArgumentNullException(nameof(value));

            _name = name;
            _value = value;
        }

        /// <summary>
        /// Gets the items associated with the specified names.
        /// </summary>
        /// <param name="names">The names of the item/s to get.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
        /// <returns>
        /// The items associated with the specified <param name="names"></param>. 
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="names"/> is <c>null</c>.</exception> 
        /// <exception cref="SmartEnumNotFoundException"><paramref name="names"/> does not exist.</exception> 
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.TryFromName(string, out TEnum)"/>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
        public static IEnumerable<TEnum> FromName(string names, bool ignoreCase = false, bool deserialize = false)
        {
            if (String.IsNullOrEmpty(names))
                ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(names));

            if (ignoreCase)
                return FromName(_fromNameIgnoreCase.Value);
            else
                return FromName(_fromName.Value);

            IEnumerable<TEnum> FromName(Dictionary<string, TEnum> dictionary)
            {
                if (!dictionary.TryGetFlagEnumValuesByName<TEnum, TValue>(names, out var result))
                {
                    ThrowHelper.ThrowNameNotFoundException<TEnum, TValue>(names);
                }
                return result;
            }
        }

        /// <summary>
        /// Gets the items associated with the specified <paramref name="names"/>.
        /// </summary>
        /// <param name="names">The names of the item/s to get.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified names, if the key is found; 
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="SmartFlagEnum{TEnum, TValue}"/> contains an item with the specified names; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref names="names"/> is <c>null</c>.</exception> 
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.FromName(string, bool)"/>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFromName(string names, out IEnumerable<TEnum> result) =>
            TryFromName(names, false, out result);

        /// <summary>
        /// Gets the items associated with the specified <paramref name="names"/>.
        /// </summary>
        /// <param name="names">The names of the item/s to get.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
        /// <param name="result">
        /// When this method returns, contains the items associated with the specified names, if any names are found; 
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="SmartFlagEnum{TEnum, TValue}"/> contains an item with the specified names; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref names="names"/> is <c>null</c>.</exception> 
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.FromName(string, bool)"/>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.TryFromName(string, out TEnum)"/>
        public static bool TryFromName(string names, bool ignoreCase, out IEnumerable<TEnum> result)
        {
            if (String.IsNullOrEmpty(names))
                ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(names));

            if (ignoreCase)
                return _fromNameIgnoreCase.Value.TryGetFlagEnumValuesByName<TEnum, TValue>(names, out result);
            else
                return _fromName.Value.TryGetFlagEnumValuesByName<TEnum, TValue>(names, out result);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{TEnum}"/> associated with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the item/s to get.</param>
        /// <returns>
        /// An <see cref="IEnumerable{TEnum}"/> containing the item/s found.
        /// </returns>
        /// <exception cref="SmartEnumNotFoundException"><paramref name="value"/> does not exist.</exception> 
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.FromValue(TValue, TEnum)"/>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
        public static IEnumerable<TEnum> FromValue(TValue value)
        {
            if (value == null)
                ThrowHelper.ThrowArgumentNullException(nameof(value));

            if (GetFlagEnumValues(value, GetAllOptions()) == null)
            {
                ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
            }

            return GetFlagEnumValues(value, GetAllOptions());
        }

        /// <summary>
        /// Attempts to retrieve a single <see cref="SmartFlagEnum{TEnum}"/> by value.  (Bypasses all Flag behaviour)
        /// </summary>
        /// <exception cref="SmartEnumNotFoundException"><paramref name="value"/> does not exist.</exception> 
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum DeserializeValue(TValue value)
        {
            var enumList = GetAllOptions();

            foreach (var smartFlagEnum in enumList)
            {
                if (smartFlagEnum.Value.Equals(value))
                {
                    return smartFlagEnum;
                }
            }

            ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);

            return null;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{TEnum}"/> associated with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the item/s to get.</param>
        /// <param name="defaultValue">The value to return when no items found.</param>
        /// <returns>
        /// An <see cref="IEnumerable{TEnum}"/> containing the item/s found.
        /// If the specified value is not found, returns <paramref name="defaultValue"/>.
        /// </returns>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.FromValue(TValue)"/>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
        public static IEnumerable<TEnum> FromValue(TValue value, IEnumerable<TEnum> defaultValue)
        {
            if (value == null)
                ThrowHelper.ThrowArgumentNullException(nameof(value));

            return !TryFromValue(value, out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{TEnum}"/> associated with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value of the item/s to get.</param>
        /// <param name="result">
        /// When this method returns, contains the IEnumerable of item/s associated with the specified value, if any value is found; 
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="SmartFlagEnum{TEnum, TValue}"/> contains any items with the specified names; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.FromValue(TValue)"/>
        /// <seealso cref="SmartFlagEnum{TEnum, TValue}.FromValue(TValue, TEnum)"/>
        public static bool TryFromValue(TValue value, out IEnumerable<TEnum> result)
        {
            if (value == null || !int.TryParse(value.ToString(), out _))
            {
                result = default;
                return false;
            }


            result = GetFlagEnumValues(value, GetAllOptions());
            if (result == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of a given <paramref name="value"/> as a series of comma delineated enum names.
        /// </summary>
        /// <param name="value">The value of the item/s to get.</param>
        /// <returns>A comma delineated series of enum names as a <see cref="string"/></returns>
        public static string FromValueToString(TValue value)
        {
            if (!TryFromValue(value, out _))
            {
                ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
            }

            return FormatEnumListString(GetFlagEnumValues(value, GetAllOptions()));
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of a given <paramref name="value"/> as a series of comma delineated enum names.
        /// </summary>
        /// <param name="value">The value of the item/s to get.</param>
        /// <param name="result">
        /// When this method returns, contains the string representation associated with the specified value, if any value is found; 
        /// otherwise, returns null.</param>
        /// <returns>A comma delineated series of enum names as a <see cref="string"/></returns>
        public static bool TryFromValueToString(TValue value, out string result)
        {
            if (!TryFromValue(value, out var enumResult))
            {
                result = default;
                return false;
            }

            result = FormatEnumListString(GetFlagEnumValues(value, enumResult));
            return true;
        }

        private static string FormatEnumListString(IEnumerable<TEnum> enumInputList)
        {
            var enumList = enumInputList.ToList();
            var sb = new StringBuilder();

            foreach (var smartFlagEnum in enumList)
            {
                sb.Append(smartFlagEnum.Name);
                if (enumList.Last().Name != smartFlagEnum.Name && enumList.Count > 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        public override string ToString() =>
            _name;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() =>
            _value.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <param names="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            (obj is SmartFlagEnum<TEnum, TValue> other) && Equals(other);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="SmartFlagEnum{TEnum, TValue}"/> value.
        /// </summary>
        /// <param name="other">An <see cref="SmartFlagEnum{TEnum, TValue}"/> value to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
        public virtual bool Equals(SmartFlagEnum<TEnum, TValue> other)
        {
            // check if same instance
            if (Object.ReferenceEquals(this, other))
                return true;

            // it's not same instance so 
            // check if it's not null and is same value
            if (other is null)
                return false;

            return _value.Equals(other._value);
        }

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartFlagEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartFlagEnumWhen">A collection of <see cref="SmartFlagEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(SmartFlagEnum<TEnum, TValue> smartFlagEnumWhen) =>
            new SmartEnumThen<TEnum, TValue>(this.Equals(smartFlagEnumWhen), false, this);

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartFlagEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartFlagEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(params SmartFlagEnum<TEnum, TValue>[] smartEnums) =>
            new SmartEnumThen<TEnum, TValue>(smartEnums.Contains(this), false, this);

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartFlagEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartFlagEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(IEnumerable<SmartFlagEnum<TEnum, TValue>> smartEnums) =>
            new SmartEnumThen<TEnum, TValue>(smartEnums.Contains(this), false, this);

        public static bool operator ==(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right)
        {
            // Handle null on left side
            if (left is null)
                return right is null; // null == null = true

            // Equals handles null on right side
            return left.Equals(right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right) =>
            !(left == right);

        /// <summary>
        /// Compares this instance to a specified <see cref="SmartFlagEnum{TEnum, TValue}"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An <see cref="SmartFlagEnum{TEnum, TValue}"/> value to compare to this instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="other"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual int CompareTo(SmartFlagEnum<TEnum, TValue> other) =>
            _value.CompareTo(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right) =>
            left.CompareTo(right) < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right) =>
            left.CompareTo(right) <= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right) =>
            left.CompareTo(right) > 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right) =>
            left.CompareTo(right) >= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator TValue(SmartFlagEnum<TEnum, TValue> smartFlagEnum) =>
            smartFlagEnum._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator SmartFlagEnum<TEnum, TValue>(TValue value) =>
            FromValue(value).First();
    }
}
