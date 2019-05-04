using System;

namespace Ardalis.SmartEnum.Core
{
    public class Execute<TEnum, TValue> : IThen<TEnum, TValue>
            where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        private readonly SmartEnum<TEnum, TValue> smartEnum;

        public Execute(SmartEnum<TEnum, TValue> smartEnum)
        {
            this.smartEnum = smartEnum;
        }

        public SmartEnum<TEnum, TValue> Then(Action doThis)
        {
            doThis();
            return smartEnum;
        }
    }
}
