using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using Ardalis.SmartEnum.Exceptions;

namespace Ardalis.SmartEnum
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class SmartFlagEngine<TEnum, TValue>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        protected SmartFlagEngine() { }

        /// <summary>
        /// Returns an <see cref="IEnumerable{TEnum}"/> representing a given <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to retrieve.</param>
        /// <param name="allEnumList">an <see cref="IEnumerable{T}"/> of <see cref="SmartFlagEnum{TEnum}"/> from which to retrieve values.</param>
        /// <returns></returns>
        [SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>")]
        protected static IEnumerable<TEnum> GetFlagEnumValues(TValue value, IEnumerable<TEnum> allEnumList)
        {
            GuardAgainstNull(value);
            GuardAgainstInvalidInputValue(value);
            GuardAgainstNegativeInputValue(value);

            var inputValueAsInt = int.Parse(value.ToString());
            var enumFlagStateDictionary = new Dictionary<TEnum, bool>();
            var inputEnumList = allEnumList.ToList();

            ApplyUnsafeFlagEnumAttributeSettings(inputEnumList);

            var maximumAllowedValue = CalculateHighestAllowedFlagValue(inputEnumList);
            
            var typeMaxValue = GetMaxValue();

            foreach (var enumValue in inputEnumList)
            {
                var currentEnumValueAsInt = int.Parse(enumValue.Value.ToString());

                CheckEnumForNegativeValues(currentEnumValueAsInt);

                if (currentEnumValueAsInt == inputValueAsInt)
                    return new List<TEnum> { enumValue };

                if (inputValueAsInt == -1 || value.Equals(typeMaxValue))
                {
                    return inputEnumList.Where(x => long.Parse(x.Value.ToString()) > 0);
                }

                AssignFlagStateValuesToDictionary(inputValueAsInt, currentEnumValueAsInt, enumValue, enumFlagStateDictionary);
            }

            return inputValueAsInt > maximumAllowedValue ? default : CreateSmartEnumReturnList(enumFlagStateDictionary);
        }

        private static void GuardAgainstNull(TValue value)
        {
            if (value is null)
                ThrowHelper.ThrowArgumentNullException(nameof(value));
        }

        private static void GuardAgainstInvalidInputValue(TValue value)
        {
            if (!int.TryParse(value.ToString(), out _))
                ThrowHelper.ThrowInvalidValueCastException<TEnum, TValue>(value);
        }

        private static void GuardAgainstNegativeInputValue(TValue value)
        {
            AllowNegativeInputValuesAttribute attribute = (AllowNegativeInputValuesAttribute)
                Attribute.GetCustomAttribute(typeof(TEnum), typeof(AllowNegativeInputValuesAttribute));

            if (attribute is null && int.Parse(value.ToString()) < -1)
            {
                ThrowHelper.ThrowNegativeValueArgumentException<TEnum, TValue>(value);
            }
        }

        private static void CheckEnumForNegativeValues(int value)
        {
            if (value < -1)
                ThrowHelper.ThrowContainsNegativeValueException<TEnum, TValue>();
        }

        private static int CalculateHighestAllowedFlagValue(List<TEnum> inputEnumList)
        {
            return (HighestFlagValue(inputEnumList) * 2) - 1;
        }

        private static void AssignFlagStateValuesToDictionary(int inputValueAsInt, int currentEnumValue, TEnum enumValue, IDictionary<TEnum, bool> enumFlagStateDictionary)
        {
            if (!enumFlagStateDictionary.ContainsKey(enumValue) && currentEnumValue != 0)
            {
                bool flagState = (inputValueAsInt & currentEnumValue) == currentEnumValue;
                enumFlagStateDictionary.Add(enumValue, flagState);
            }
        }

        private static IEnumerable<TEnum> CreateSmartEnumReturnList(Dictionary<TEnum, bool> enumFlagStateDictionary)
        {
            var outputList = new List<TEnum>();

            foreach (var entry in enumFlagStateDictionary)
            {
                if (entry.Value)
                {
                    outputList.Add(entry.Key);
                }
            }

            return outputList.DefaultIfEmpty();
        }

        private static void ApplyUnsafeFlagEnumAttributeSettings(IEnumerable<TEnum> list)
        {
            AllowUnsafeFlagEnumValuesAttribute attribute = (AllowUnsafeFlagEnumValuesAttribute)
                Attribute.GetCustomAttribute(typeof(TEnum), typeof(AllowUnsafeFlagEnumValuesAttribute));

            if (attribute is null)
            {
                CheckEnumListForPowersOfTwo(list);
            }
        }

        private static void CheckEnumListForPowersOfTwo(IEnumerable<TEnum> enumEnumerable)
        {
            var enumList = enumEnumerable.ToList();
            var enumValueList = new List<int>();
            foreach (var smartFlagEnum in enumList)
            {
                enumValueList.Add(int.Parse(smartFlagEnum.Value.ToString()));
            }
            var firstPowerOfTwoValue = 0;
            if (int.Parse(enumList[0].Value.ToString()) == 0)
            {
                enumList.RemoveAt(0);
            }

            foreach (var flagEnum in enumList)
            {
                var x = int.Parse(flagEnum.Value.ToString());
                if (IsPowerOfTwo(x))
                {
                    firstPowerOfTwoValue = x;
                    break;
                }
            }

            var highestValue = HighestFlagValue(enumList);
            var currentValue = firstPowerOfTwoValue;

            while (currentValue != highestValue)
            {
                var nextPowerOfTwoValue = currentValue * 2;
                var result = enumValueList.BinarySearch(nextPowerOfTwoValue);
                if (result < 0)
                {
                    ThrowHelper.ThrowDoesNotContainPowerOfTwoValuesException<TEnum, TValue>();
                }

                currentValue = nextPowerOfTwoValue;
            }
        }

        private static bool IsPowerOfTwo(int input)
        {
            if (input != 0 && ((input & (input - 1)) == 0))
            {
                return true;
            }

            return false;
        }

        [SuppressMessage("Performance", "CA1826:Do not use Enumerable methods on indexable collections", Justification = "<Pending>")]
        [SuppressMessage("Minor Code Smell", "S6608:Prefer indexing instead of \"Enumerable\" methods on types implementing \"IList\"", Justification = "<Pending>")]
        private static int HighestFlagValue(IReadOnlyList<TEnum> enumList)
        {
            var highestIndex = enumList.Count - 1;
            var highestValue = int.Parse(enumList.Last().Value.ToString());
            if (!IsPowerOfTwo(highestValue))
            {
                for (var i = highestIndex; i >= 0; i--)
                {
                    var currentValue = int.Parse(enumList[i].Value.ToString());
                    if (IsPowerOfTwo(currentValue))
                    {
                        highestValue = currentValue;
                        break;
                    }
                }
            }

            return highestValue;
        }

        /// <summary>
        /// Gets the largest possible value of the underlying type for the SmartFlagEnum.
        /// </summary>
        /// <exception cref="NotSupportedException">If the underlying type <typeparamref name="TValue"/>
        /// does not define a <c>MaxValue</c> field, this exception is thrown.
        /// </exception>
        /// <returns>The value of the constant <c>MaxValue</c> field defined by the underlying type <typeparamref name="TValue"/>.</returns>
        private static TValue GetMaxValue()
        {
            FieldInfo maxValueField = typeof(TValue).GetField("MaxValue", BindingFlags.Public
                | BindingFlags.Static);
            if (maxValueField == null)
                throw new NotSupportedException(typeof(TValue).Name);

            TValue maxValue = (TValue)maxValueField.GetValue(null);

            return maxValue;
        }
    }
}
