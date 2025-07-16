using System;

namespace Ardalis.SmartEnum.ProtoBufNet.UnitTests
{
    public sealed class FlagTestEnumInt16 : SmartFlagEnum<FlagTestEnumInt16, short>
    {
        public static readonly FlagTestEnumInt16 Instance = new FlagTestEnumInt16(nameof(Instance), 1);

        private FlagTestEnumInt16(string name, short value) : base(name, value) { }
    }

    public sealed class FlagTestEnumInt32 : SmartFlagEnum<FlagTestEnumInt32, int>
    {
        public static readonly FlagTestEnumInt32 Instance = new FlagTestEnumInt32(nameof(Instance), 1);
        public static readonly FlagTestEnumInt32 Instance2 = new FlagTestEnumInt32(nameof(Instance2), 2);
        public static readonly FlagTestEnumInt32 Instance3 = new FlagTestEnumInt32(nameof(Instance3), 4);

        private FlagTestEnumInt32(string name, int value) : base(name, value) { }
    }

    public sealed class FlagTestEnumDouble : SmartFlagEnum<FlagTestEnumDouble, double>
    {
        public static readonly FlagTestEnumDouble Instance = new FlagTestEnumDouble(nameof(Instance), 1.0);

        private FlagTestEnumDouble(string name, double value) : base(name, value) { }
    }

    public sealed class FlagTestEnumGuid : SmartFlagEnum<FlagTestEnumGuid, Guid>
    {
        public static readonly FlagTestEnumGuid Instance = new FlagTestEnumGuid(nameof(Instance), new Guid("00000000-1111-2222-3333-444444444444"));

        private FlagTestEnumGuid(string name, Guid value) : base(name, value) { }
    }
}
