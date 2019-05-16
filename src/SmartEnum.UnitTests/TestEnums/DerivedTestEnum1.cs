namespace SmartEnum.UnitTests.TestEnums
{
    internal class DerivedTestEnum1 : BaseTestEnum
    {
        private DerivedTestEnum1(string name, int value) : base(name, value) { }

        public static readonly DerivedTestEnum1 A = new DerivedTestEnum1(nameof(A), 1);
        public static readonly DerivedTestEnum1 B = new DerivedTestEnum1(nameof(B), 2);
    }
}
