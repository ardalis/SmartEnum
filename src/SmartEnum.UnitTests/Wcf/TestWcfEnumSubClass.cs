namespace SmartEnum.UnitTests.Wcf
{
    public class TestWcfEnumSubClass : TestWcfEnum
    {
        public static TestWcfEnumSubClass Four = new TestWcfEnumSubClass(nameof(Four), 1);
        public static TestWcfEnumSubClass Five = new TestWcfEnumSubClass(nameof(Five), 2);
        public static TestWcfEnumSubClass Six = new TestWcfEnumSubClass(nameof(Six), 3);

        protected TestWcfEnumSubClass(string name, int value) : base(name, value)
        {
        }

        private TestWcfEnumSubClass() :base()
        {
            // required for EF
        }
    }
}
