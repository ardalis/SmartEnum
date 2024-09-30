using System.Runtime.InteropServices.ComTypes;

namespace Ardalis.SmartEnum
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    using Ardalis.SmartEnum.Core;

    /// <summary>
    /// A base type to use for creating smart enums with inner value of type <see cref="System.Int32"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
    /// <remarks></remarks>
    public abstract class SmartEnum<TEnum> :
        SmartEnum<TEnum, int>
        where TEnum : SmartEnum<TEnum, int>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected SmartEnum(string name, int value) :
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
    public abstract class SmartEnum<TEnum, TValue> :
        ISmartEnum,
        IEquatable<SmartEnum<TEnum, TValue>>,
        IComparable<SmartEnum<TEnum, TValue>>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        static readonly Lazy<TEnum[]> _enumOptions =
            new Lazy<TEnum[]>(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

        static readonly Lazy<Dictionary<string, TEnum>> _fromName =
            new Lazy<Dictionary<string, TEnum>>(() => _enumOptions.Value.ToDictionary(item => item.Name));

        static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase =
            new Lazy<Dictionary<string, TEnum>>(() => _enumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

        static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue =
            new Lazy<Dictionary<TValue, TEnum>>(() =>
            {
                // multiple enums with same value are allowed but store only one per value
                var dictionary = new Dictionary<TValue, TEnum>(GetValueComparer());
                foreach (var item in _enumOptions.Value)
                {
                    if (item._value != null && !dictionary.ContainsKey(item._value))
                        dictionary.Add(item._value, item);
                }
                return dictionary;
            });

        private static TEnum[] GetAllOptions()
        {
            Type baseType = typeof(TEnum);
            return Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t))
                .SelectMany(t => t.GetFieldsOfType<TEnum>())
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private static IEqualityComparer<TValue> GetValueComparer()
        {
            var comparer = typeof(TEnum).GetCustomAttribute<SmartEnumComparerAttribute<TValue>>();
            return comparer?.Comparer;
        }

        /// <summary>
        /// Gets a collection containing all the instances of <see cref="SmartEnum{TEnum, TValue}"/>.
        /// </summary>
        /// <value>A <see cref="IReadOnlyCollection{TEnum}"/> containing all the instances of <see cref="SmartEnum{TEnum, TValue}"/>.</value>
        /// <remarks>Retrieves all the instances of <see cref="SmartEnum{TEnum, TValue}"/> referenced by public static read-only fields in the current class or its bases.</remarks>
        public static IReadOnlyCollection<TEnum> List => _enumOptions.Value;

        private readonly string _name;
        private readonly TValue _value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected SmartEnum(string name, TValue value)
        {
            if (String.IsNullOrEmpty(name))
                ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));

            _name = name;
            _value = value;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>A <see cref="String"/> that is the name of the <see cref="SmartEnum{TEnum, TValue}"/>.</value>
        public string Name =>
            _name;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>A <typeparamref name="TValue"/> that is the value of the <see cref="SmartEnum{TEnum, TValue}"/>.</value>
        public TValue Value =>
            _value;


        /// <summary>
        /// Gets the item associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
        /// <returns>
        /// The item associated with the specified name.
        /// If the specified name is not found, throws a <see cref="KeyNotFoundException"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <exception cref="SmartEnumNotFoundException"><paramref name="name"/> does not exist.</exception>
        /// <seealso cref="SmartEnum{TEnum, TValue}.TryFromName(string, out TEnum)"/>
        /// <seealso cref="SmartEnum{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
        public static TEnum FromName(string name, bool ignoreCase = false)
        {
            if (String.IsNullOrEmpty(name))
                ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));

            if (ignoreCase)
                return FromName(_fromNameIgnoreCase.Value);
            else
                return FromName(_fromName.Value);

            TEnum FromName(Dictionary<string, TEnum> dictionary)
            {
                if (!dictionary.TryGetValue(name, out var result))
                {
                    ThrowHelper.ThrowNameNotFoundException<TEnum, TValue>(name);
                }
                return result;
            }
        }

        /// <summary>
        /// Gets the item associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified name, if the key is found;
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="SmartEnum{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <seealso cref="SmartEnum{TEnum, TValue}.FromName(string, bool)"/>
        /// <seealso cref="SmartEnum{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFromName(string name, out TEnum result) =>
            TryFromName(name, false, out result);

        /// <summary>
        /// Gets the item associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified name, if the name is found;
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="SmartEnum{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <seealso cref="SmartEnum{TEnum, TValue}.FromName(string, bool)"/>
        /// <seealso cref="SmartEnum{TEnum, TValue}.TryFromName(string, out TEnum)"/>
        public static bool TryFromName(string name, bool ignoreCase, out TEnum result)
        {
            if (String.IsNullOrEmpty(name))
            {
                result = default;
                return false;
            }

            if (ignoreCase)
                return _fromNameIgnoreCase.Value.TryGetValue(name, out result);
            else
                return _fromName.Value.TryGetValue(name, out result);
        }

        /// <summary>
        /// Gets an item associated with the specified value.
        /// </summary>
        /// <param name="value">The value of the item to get.</param>
        /// <returns>
        /// The first item found that is associated with the specified value.
        /// If the specified value is not found, throws a <see cref="KeyNotFoundException"/>.
        /// </returns>
        /// <exception cref="SmartEnumNotFoundException"><paramref name="value"/> does not exist.</exception>
        /// <seealso cref="SmartEnum{TEnum, TValue}.FromValue(TValue, TEnum)"/>
        /// <seealso cref="SmartEnum{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
        [SuppressMessage("Minor Code Smell", "S6602:\"Find\" method should be used instead of the \"FirstOrDefault\" extension", Justification = "<Pending>")]
        public static TEnum FromValue(TValue value)
        {
            TEnum result;

            if (value != null)
            {
                if (!_fromValue.Value.TryGetValue(value, out result))
                {
                    ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
                }
            }
            else
            {
                result = _enumOptions.Value.FirstOrDefault(x => x.Value == null);
                if (result == null)
                {
                    ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets an item associated with the specified value.
        /// </summary>
        /// <param name="value">The value of the item to get.</param>
        /// <param name="defaultValue">The value to return when item not found.</param>
        /// <returns>
        /// The first item found that is associated with the specified value.
        /// If the specified value is not found, returns <paramref name="defaultValue"/>.
        /// </returns>
        /// <seealso cref="SmartEnum{TEnum, TValue}.FromValue(TValue)"/>
        /// <seealso cref="SmartEnum{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
        public static TEnum FromValue(TValue value, TEnum defaultValue)
        {
            if (value == null)
                ThrowHelper.ThrowArgumentNullException(nameof(value));

            if (!_fromValue.Value.TryGetValue(value, out var result))
            {
                return defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Gets an item associated with the specified value.
        /// </summary>
        /// <param name="value">The value of the item to get.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified value, if the value is found;
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="SmartEnum{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="SmartEnum{TEnum, TValue}.FromValue(TValue)"/>
        /// <seealso cref="SmartEnum{TEnum, TValue}.FromValue(TValue, TEnum)"/>
        public static bool TryFromValue(TValue value, out TEnum result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }

            return _fromValue.Value.TryGetValue(value, out result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            _name;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() =>
            _value.GetHashCode();

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            (obj is SmartEnum<TEnum, TValue> other) && Equals(other);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="SmartEnum{TEnum, TValue}"/> value.
        /// </summary>
        /// <param name="other">An <see cref="SmartEnum{TEnum, TValue}"/> value to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
        public virtual bool Equals(SmartEnum<TEnum, TValue> other)
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
        /// When this instance is one of the specified <see cref="SmartEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnumWhen">A collection of <see cref="SmartEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(SmartEnum<TEnum, TValue> smartEnumWhen) =>
            new SmartEnumThen<TEnum, TValue>(this.Equals(smartEnumWhen), false, this);

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(params SmartEnum<TEnum, TValue>[] smartEnums) =>
            new SmartEnumThen<TEnum, TValue>(smartEnums.Contains(this), false, this);

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(IEnumerable<SmartEnum<TEnum, TValue>> smartEnums) =>
            new SmartEnumThen<TEnum, TValue>(smartEnums.Contains(this), false, this);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right)
        {
            // Handle null on left side
            if (left is null)
                return right is null; // null == null = true

            // Equals handles null on right side
            return left.Equals(right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) =>
            !(left == right);

        /// <summary>
        /// Compares this instance to a specified <see cref="SmartEnum{TEnum, TValue}"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An <see cref="SmartEnum{TEnum, TValue}"/> value to compare to this instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="other"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual int CompareTo(SmartEnum<TEnum, TValue> other) =>
            _value.CompareTo(other._value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) =>
            left.CompareTo(right) < 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) =>
            left.CompareTo(right) <= 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) =>
            left.CompareTo(right) > 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) =>
            left.CompareTo(right) >= 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="smartEnum"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator TValue(SmartEnum<TEnum, TValue> smartEnum) =>
            smartEnum is not null
                ? smartEnum._value
                : default;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator SmartEnum<TEnum, TValue>(TValue value) =>
            FromValue(value);
    }
}
