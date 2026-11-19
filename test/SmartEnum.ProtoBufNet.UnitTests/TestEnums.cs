using System;

namespace Ardalis.SmartEnum.ProtoBufNet.UnitTests
{
    public sealed class TestEnumBoolean : SmartEnum<TestEnumBoolean, bool>
    {
        public static readonly TestEnumBoolean Instance = new TestEnumBoolean(nameof(Instance), true);

        private TestEnumBoolean(string name, bool value) : base(name, value) { }
    }

    public sealed class TestEnumInt16 : SmartEnum<TestEnumInt16, short>
    {
        public static readonly TestEnumInt16 Instance = new TestEnumInt16(nameof(Instance), 1);

        private TestEnumInt16(string name, short value) : base(name, value) { }
    }

    public sealed class TestEnumInt32 : SmartEnum<TestEnumInt32, int>
    {
        public static readonly TestEnumInt32 Instance = new TestEnumInt32(nameof(Instance), 1);

        private TestEnumInt32(string name, int value) : base(name, value) { }
    }

    public sealed class TestEnumDouble : SmartEnum<TestEnumDouble, double>
    {
        public static readonly TestEnumDouble Instance = new TestEnumDouble(nameof(Instance), 1.0);

        private TestEnumDouble(string name, double value) : base(name, value) { }
    }

    public sealed class TestEnumGuid : SmartEnum<TestEnumGuid, Guid>
    {
        public static readonly TestEnumGuid Instance = new TestEnumGuid(nameof(Instance), new Guid("00000000-1111-2222-3333-444444444444"));

        private TestEnumGuid(string name, Guid value) : base(name, value) { }
    }
}
