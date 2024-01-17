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
    public class SmartEnumNameSurrogate<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        [ProtoMember(1, IsRequired = true)]
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smartEnum"></param>
        public static implicit operator SmartEnumNameSurrogate<TEnum, TValue>(TEnum smartEnum)
        {
            if (smartEnum is null)
                return null;

            return new SmartEnumNameSurrogate<TEnum, TValue> { Name = smartEnum.Name };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="surrogate"></param>
        public static implicit operator TEnum(SmartEnumNameSurrogate<TEnum, TValue> surrogate)
        {
            if (surrogate is null)
                return null;

            return SmartEnum<TEnum, TValue>.FromName(surrogate.Name);
        }
    }
}