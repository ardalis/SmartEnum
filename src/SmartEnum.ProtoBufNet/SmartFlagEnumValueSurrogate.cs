namespace Ardalis.SmartEnum.ProtoBufNet
{
    using System;
    using ProtoBuf;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [ProtoContract]
    public class SmartFlagEnumValueSurrogate<TEnum, TValue>
        where TEnum : SmartFlagEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        [ProtoMember(1, IsRequired = true)]
        TValue Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smartEnum"></param>
        public static implicit operator SmartFlagEnumValueSurrogate<TEnum, TValue>(TEnum smartEnum)
        {
            if (smartEnum is null)
                return null;

            return new SmartFlagEnumValueSurrogate<TEnum, TValue> { Value = smartEnum.Value };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="surrogate"></param>
        public static implicit operator TEnum(SmartFlagEnumValueSurrogate<TEnum, TValue> surrogate)
        {
            if (surrogate is null)
                return null;

            return SmartFlagEnum<TEnum, TValue>.DeserializeValue(surrogate.Value);
        }
    }
}
