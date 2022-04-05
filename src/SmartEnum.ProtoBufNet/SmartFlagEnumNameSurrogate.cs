using System;
using System.Linq;
using ProtoBuf;

namespace Ardalis.SmartEnum.ProtoBufNet
{
    [ProtoContract]
    public class SmartFlagEnumNameSurrogate<TEnum, TValue>
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        [ProtoMember(1, IsRequired =  true)] 
        public string Name { get; set; }

        public static implicit operator SmartFlagEnumNameSurrogate<TEnum, TValue>(TEnum smartFlagEnum)
        {
            if (smartFlagEnum is null)
                return null;

            return new SmartFlagEnumNameSurrogate<TEnum, TValue> { Name = smartFlagEnum.Name };
        }

        public static implicit operator TEnum(SmartFlagEnumNameSurrogate<TEnum, TValue> surrogate)
        {
            if (surrogate is null)
                return null;

            return SmartFlagEnum<TEnum, TValue>.FromName(surrogate.Name).FirstOrDefault();
        }
    }
}
