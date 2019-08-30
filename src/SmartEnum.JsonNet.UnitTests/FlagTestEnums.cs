using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Ardalis.SmartEnum;

namespace SmartEnum.JsonNet.UnitTests
{
    public class FlagTestEnums
    {
        public sealed class FlagTestEnumInt16 : SmartFlagEnum<FlagTestEnumInt16, short>
        {
            public static readonly FlagTestEnumInt16 One = new FlagTestEnumInt16(nameof(One), 1);
            public static readonly FlagTestEnumInt16 Two = new FlagTestEnumInt16(nameof(Two), 2);
            public static readonly FlagTestEnumInt16 Three = new FlagTestEnumInt16(nameof(Three), 4);

            FlagTestEnumInt16(string name, short value) : base(name, value) { }
        }

        public sealed class FlagTestEnumInt32 : SmartFlagEnum<FlagTestEnumInt32, int>
        {
            public static readonly FlagTestEnumInt32 One = new FlagTestEnumInt32(nameof(One), 1);
            public static readonly FlagTestEnumInt32 Two = new FlagTestEnumInt32(nameof(Two), 2);
            public static readonly FlagTestEnumInt32 Three = new FlagTestEnumInt32(nameof(Three), 4);
            public static readonly FlagTestEnumInt32 Four = new FlagTestEnumInt32(nameof(Four), 8);

            FlagTestEnumInt32(string name, int value) : base(name, value) { }
        }

        public sealed class FlagTestEnumDouble : SmartFlagEnum<FlagTestEnumDouble, double>
        {
            public static readonly FlagTestEnumDouble One = new FlagTestEnumDouble(nameof(One), 1.0);
            public static readonly FlagTestEnumDouble Two = new FlagTestEnumDouble(nameof(Two), 2.0);
            public static readonly FlagTestEnumDouble Three = new FlagTestEnumDouble(nameof(Three), 4.0);


            FlagTestEnumDouble(string name, double value) : base(name, value) { }
        }
    }
}
