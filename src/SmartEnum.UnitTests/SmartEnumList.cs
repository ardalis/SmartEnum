using Ardalis.SmartEnum;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumList
    {
        [Fact]
        public void ReturnsAllDefinedSmartEnums()
        {
            var result = TestEnum.List;

            Assert.Equal(3, result.Count);
            Assert.Contains(TestEnum.One, result);
            Assert.Contains(TestEnum.Two, result);
            Assert.Contains(TestEnum.Three, result);
        }

        [Fact]
        public void ReturnsAllEnumsFromDerivedTypes()
        {
            var result = BaseEnum.List;

            Assert.Equal(4, result.Count);
            Assert.Contains(DerivedEnum1.A, result);
            Assert.Contains(DerivedEnum1.B, result);
            Assert.Contains(DerivedEnum2.C, result);
            Assert.Contains(DerivedEnum2.D, result);
        }

        private abstract class BaseEnum : SmartEnum<BaseEnum, int>
        {
            protected BaseEnum(string name, int value) : base(name, value) { }
        }

        private class DerivedEnum1 : BaseEnum
        {
            private DerivedEnum1(string name, int value) : base(name, value) { }

            public static readonly DerivedEnum1 A = new DerivedEnum1("A", 1);
            public static readonly DerivedEnum1 B = new DerivedEnum1("B", 2);
        }

        private class DerivedEnum2 : BaseEnum
        {
            private DerivedEnum2(string name, int value) : base(name, value) { }

            public static readonly DerivedEnum2 C = new DerivedEnum2("C", 3);
            public static readonly DerivedEnum2 D = new DerivedEnum2("D", 4);
        }
    }
}
