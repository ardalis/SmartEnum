using System;

namespace Ardalis.SmartEnum.Core
{
    public readonly struct SmartEnumThen<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        private readonly bool isMatch;
        private readonly SmartEnum<TEnum, TValue> smartEnum;
        private readonly bool stopEvaluating;

        internal SmartEnumThen(bool isMatch, bool stopEvaluating, SmartEnum<TEnum, TValue> smartEnum)
        {
            this.isMatch = isMatch;
            this.smartEnum = smartEnum;
            this.stopEvaluating = stopEvaluating;
        }

        /// <summary>
        /// Calls <paramref name="doThis"/> Action when the preceding When call matches.
        /// </summary>
        /// <param name="doThis">Action method to call.</param>
        /// <returns>A chainable instance of CaseWhen for more when calls.</returns>
        public SmartEnumWhen<TEnum, TValue> Then(Action doThis)
        {
            if (!stopEvaluating && isMatch)
                doThis();

            return new SmartEnumWhen<TEnum, TValue>(stopEvaluating || isMatch, smartEnum);
        }
    }
}