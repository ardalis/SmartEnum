using System.Diagnostics.CodeAnalysis;

namespace Ardalis.SmartEnum.Benchmarks
{
    ////////////////////////////////////////////////////////////////////////////////
    // Enum

    [global::ProtoBuf.ProtoContract]
    [SuppressMessage("Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes", Justification = "<Pending>")]
    public enum TestEnum
    {
        [global::ProtoBuf.ProtoEnum]
        One = 1,
        [global::ProtoBuf.ProtoEnum]
        Two = 2,
        [global::ProtoBuf.ProtoEnum]
        Three = 3,
        [global::ProtoBuf.ProtoEnum]
        Four = 4,
        [global::ProtoBuf.ProtoEnum]
        Five = 5,
        [global::ProtoBuf.ProtoEnum]
        Six = 6,
        [global::ProtoBuf.ProtoEnum]
        Seven = 7,
        [global::ProtoBuf.ProtoEnum]
        Eight = 8,
        [global::ProtoBuf.ProtoEnum]
        Nine = 9,
        [global::ProtoBuf.ProtoEnum]
        Ten = 10,
    }

    public enum TestEnum2
    {
        One = 1,
        Two = 2,
    }

    ////////////////////////////////////////////////////////////////////////////////
    // Constant

    public sealed class TestConstant : global::Constant.Constant<string, TestConstant>
    {
        [global::Constant.DefaultKey]
        public static readonly TestConstant One = new TestConstant(nameof(One));
        public static readonly TestConstant Two = new TestConstant(nameof(Two));
        public static readonly TestConstant Three = new TestConstant(nameof(Three));
        public static readonly TestConstant Four = new TestConstant(nameof(Four));
        public static readonly TestConstant Five = new TestConstant(nameof(Five));
        public static readonly TestConstant Six = new TestConstant(nameof(Six));
        public static readonly TestConstant Seven = new TestConstant(nameof(Seven));
        public static readonly TestConstant Eight = new TestConstant(nameof(Eight));
        public static readonly TestConstant Nine = new TestConstant(nameof(Nine));
        public static readonly TestConstant Ten = new TestConstant(nameof(Ten));

        TestConstant(string name) : base(name) { }
    }

    public sealed class TestConstant2 : global::Constant.Constant<string, TestConstant2>
    {
        public static readonly TestConstant2 One = new TestConstant2(nameof(One));
        public static readonly TestConstant2 Two = new TestConstant2(nameof(Two));

        TestConstant2(string name) : base(name) { }
    }

    ////////////////////////////////////////////////////////////////////////////////
    // SmartEnum

    public sealed class TestSmartEnum : SmartEnum<TestSmartEnum, int>
    {
        public static readonly TestSmartEnum One = new TestSmartEnum(nameof(One), 1);
        public static readonly TestSmartEnum Two = new TestSmartEnum(nameof(Two), 2);
        public static readonly TestSmartEnum Three = new TestSmartEnum(nameof(Three), 3);
        public static readonly TestSmartEnum Four = new TestSmartEnum(nameof(Four), 4);
        public static readonly TestSmartEnum Five = new TestSmartEnum(nameof(Five), 5);
        public static readonly TestSmartEnum Six = new TestSmartEnum(nameof(Six), 6);
        public static readonly TestSmartEnum Seven = new TestSmartEnum(nameof(Seven), 7);
        public static readonly TestSmartEnum Eight = new TestSmartEnum(nameof(Eight), 8);
        public static readonly TestSmartEnum Nine = new TestSmartEnum(nameof(Nine), 9);
        public static readonly TestSmartEnum Ten = new TestSmartEnum(nameof(Ten), 10);

        TestSmartEnum(string name, int value) : base(name, value) { }
    }

    public sealed class TestSmartEnum2 : SmartEnum<TestSmartEnum2, int>
    {
        public static readonly TestSmartEnum2 One = new TestSmartEnum2(nameof(One), 1);
        public static readonly TestSmartEnum2 Two = new TestSmartEnum2(nameof(Two), 2);

        TestSmartEnum2(string name, int value) : base(name, value) { }
    }
}