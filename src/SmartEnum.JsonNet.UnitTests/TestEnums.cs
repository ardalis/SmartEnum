using System;

namespace Ardalis.SmartEnum.JsonNet.UnitTests
{
    public sealed class TestEnumBoolean : SmartEnum<TestEnumBoolean, bool>
    {
        public static TestEnumBoolean Instance = new TestEnumBoolean(nameof(Instance), true);

        TestEnumBoolean(string name, bool value) : base(name, value) {}
    }

    public sealed class TestEnumInt16 : SmartEnum<TestEnumInt16, short>
    {
        public static TestEnumInt16 Instance = new TestEnumInt16(nameof(Instance), 1);

        TestEnumInt16(string name, short value) : base(name, value) {}
    }

    public sealed class TestEnumInt32 : SmartEnum<TestEnumInt32, int>
    {
        public static TestEnumInt32 Instance = new TestEnumInt32(nameof(Instance), 1);

        TestEnumInt32(string name, int value) : base(name, value) {}
    }

    public sealed class TestEnumDouble : SmartEnum<TestEnumDouble, double>
    {
        public static TestEnumDouble Instance = new TestEnumDouble(nameof(Instance), 1.0);

        TestEnumDouble(string name, double value) : base(name, value) {}
    }

    public sealed class TestEnumString : SmartEnum<TestEnumString, string>
    {
        public static TestEnumString Instance = new TestEnumString(nameof(Instance), "A string!");
        public static TestEnumString Null = new TestEnumString(nameof(Null), null);

        TestEnumString(string name, string value) : base(name, value) {}
    }}
