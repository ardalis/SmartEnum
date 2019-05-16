namespace SmartEnum.UnitTests.TestEnums
{
    internal class DerivedTestEnum2 : BaseTestEnum
    {
        private DerivedTestEnum2(string name, int value) : base(name, value) { }

        public static readonly DerivedTestEnum2 C = new DerivedTestEnum2(nameof(C), 3);
        public static readonly DerivedTestEnum2 D = new DerivedTestEnum2(nameof(D), 4);
    }
}
