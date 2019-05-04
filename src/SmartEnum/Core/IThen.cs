using System;

namespace Ardalis.SmartEnum.Core
{
    public interface IThen<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        SmartEnum<TEnum, TValue> Then(Action doThis);
    }
}
