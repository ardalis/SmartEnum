using System;
using System.Collections.Generic;
using System.Linq;

namespace Ardalis.SmartEnum.Core
{
    public readonly struct SmartEnumWhen<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        private readonly SmartEnum<TEnum, TValue> smartEnum;
        private readonly bool stopEvaluating;

        public SmartEnumWhen(bool stopEvaluating, SmartEnum<TEnum, TValue> smartEnum)
        {
            this.stopEvaluating = stopEvaluating;
            this.smartEnum = smartEnum;
        }

        /// <summary>
        /// Execute this action if no other calls to When have matched.
        /// </summary>
        /// <param name="action">The Action to call.</param>
        public void Default(Action action)
        {
            if (!stopEvaluating)
            {
                action();
            }
        }

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(SmartEnum<TEnum, TValue> smartEnumWhen) =>
            new SmartEnumThen<TEnum, TValue>(isMatch: smartEnumWhen == smartEnum, stopEvaluating: stopEvaluating, smartEnum: smartEnum);

        /// <summary>
        /// When this instance is one of the specified <see cref="SmartEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(params SmartEnum<TEnum, TValue>[] smartEnums) =>
            new SmartEnumThen<TEnum, TValue>(isMatch: smartEnums.Contains(smartEnum), stopEvaluating: stopEvaluating, smartEnum: smartEnum);

        /// When this instance is one of the specified <see cref="SmartEnum{TEnum, TValue}"/> parameters.
        /// Execute the action in the subsequent call to Then().
        /// </summary>
        /// <param name="smartEnums">A collection of <see cref="SmartEnum{TEnum, TValue}"/> values to compare to this instance.</param>
        /// <returns>A executor object to execute a supplied action.</returns>
        public SmartEnumThen<TEnum, TValue> When(IEnumerable<SmartEnum<TEnum, TValue>> smartEnums) =>
            new SmartEnumThen<TEnum, TValue>(isMatch: smartEnums.Contains(smartEnum), stopEvaluating: stopEvaluating, smartEnum: smartEnum);
    }
}