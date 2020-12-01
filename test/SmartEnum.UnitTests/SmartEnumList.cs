namespace Ardalis.SmartEnum.UnitTests
{
    using FluentAssertions;
    using Xunit;
    using Ardalis.SmartEnum.UnitTests.TestData;

    public class SmartEnumList
    {
        [Fact]
        public void ReturnsAllDefinedSmartEnums()
        {
            var result = TestEnum.List;

            result.Should().BeEquivalentTo(new[] {
                TestEnum.One,
                TestEnum.Two,
                TestEnum.Three,
            });
        }

        [Fact]
        public void ReturnsAllBaseAndDerivedSmartEnums()
        {
            var result = TestBaseEnumWithDerivedValues.List;

            result.Should().BeEquivalentTo(
                DerivedTestEnumWithValues1.A,
                DerivedTestEnumWithValues1.B,
                DerivedTestEnumWithValues2.C,
                DerivedTestEnumWithValues2.D);
        }
        
        [Fact]
        public void ReturnsAllBaseAndDerivedSmartEnumsFromMultipleAssemblies()
        {
            SmartEnumOptions.Add<AnotherEnumBase>(
                typeof(AnotherEnumBase).Assembly,
                typeof(AnotherEnumDerived3).Assembly);
            
            var result = AnotherEnumBase.List;

            result.Should().BeEquivalentTo(
                AnotherEnumDerived1.A,
                AnotherEnumDerived1.B,
                AnotherEnumDerived2.C,
                AnotherEnumDerived2.D,
                AnotherEnumDerived3.F,
                AnotherEnumDerived3.G);
        }
        
        
        public class AnotherEnumDerived3 : AnotherEnumBase
        {
            public static readonly AnotherEnumDerived3 F = new AnotherEnumDerived3(nameof(F), 5);
            public static readonly AnotherEnumDerived3 G = new AnotherEnumDerived3(nameof(G), 6);

            private AnotherEnumDerived3(string name, int value) : base(name, value) { }
        }
    }
}
