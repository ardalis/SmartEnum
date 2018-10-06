using System;

namespace Ardalis.SmartEnum
{
    public interface ISmartEnum
    {
        string Name { get; }
        object Value { get; }       
        Type GetUnderlyingType();
    }

    public interface ISmartEnum<TValue> : ISmartEnum
    {
        TValue Value { get; }       
    }
}