using Xunit;

namespace SmartEnum.UnitTests
{
    public class SmartEnumValueSetter
    {
        [Fact]
        public void ValueMutates()
        {
            string expected = "Hello";
            Assert.Equal(expected, MutableTestEnum.Hello.Value);

            string newValue = "Hola";
            MutableTestEnum.Hello.SetValue(newValue);
            Assert.Equal(newValue, MutableTestEnum.Hello.Value);
        }
    }
}