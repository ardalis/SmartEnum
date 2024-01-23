using System;
using System.Linq;
using ProtoBuf;

namespace Ardalis.SmartEnum.ProtoBufNet;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEnum"></typeparam>
/// <typeparam name="TValue"></typeparam>
[ProtoContract]
public class SmartFlagEnumNameSurrogate<TEnum, TValue>
where TEnum : SmartFlagEnum<TEnum, TValue>
where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    /// <summary>
    /// 
    /// </summary>
    [ProtoMember(1, IsRequired =  true)] 
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="smartFlagEnum"></param>
    public static implicit operator SmartFlagEnumNameSurrogate<TEnum, TValue>(TEnum smartFlagEnum)
    {
        if (smartFlagEnum is null)
            return null;

        return new SmartFlagEnumNameSurrogate<TEnum, TValue> { Name = smartFlagEnum.Name };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="surrogate"></param>
    public static implicit operator TEnum(SmartFlagEnumNameSurrogate<TEnum, TValue> surrogate)
    {
        if (surrogate is null)
            return null;

        return SmartFlagEnum<TEnum, TValue>.FromName(surrogate.Name).FirstOrDefault();
    }
}
