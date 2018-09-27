using Xunit;

namespace SmartEnum.UnitTests.Wcf
{

    public class SmartEnumWcfSerialization
    {
        [Fact]
        public void SurvivesSerializationRoundTrip()
        {
            var result = WcfTestHelper.DataContractSerializationRoundTrip(TestWcfEnum.One);


            Assert.Equal(TestWcfEnum.One, result);
        }

        [Fact]
        public void SurvivesSerializationRoundTripAsPartOfAnotherObject()
        {
            var expected = new TestObjectWithTestEnumProperty { Test = TestWcfEnum.One };
            
            var result = WcfTestHelper.DataContractSerializationRoundTrip(expected);


            Assert.Equal(expected.Test, result.Test);
        }

        [Fact]
        public void SurvivesSerializationRoundTripAsPartOfAnotherObjectAsADerivedType()
        {
            var expected = new TestObjectWithSmartEnumProperty { Test = TestWcfEnum.One };
            
            var result = WcfTestHelper.DataContractSerializationRoundTrip(expected);


            Assert.Equal(expected.Test, result.Test);
        }

        [Fact]
        public void SurvivesSerializationRoundTripAsPartOfAnotherObjectAsADerivedTypeOfADerivedType()
        {
            var expectedUsingInheritedValue = new TestObjectWithSmartEnumProperty { Test = TestWcfEnumSubClass.One };
            var expectedUsingNewValue = new TestObjectWithSmartEnumProperty { Test = TestWcfEnumSubClass.Four };
            
            var resultUsingInheritedValue = WcfTestHelper.DataContractSerializationRoundTrip(expectedUsingInheritedValue);
            var resultUsingNewValue = WcfTestHelper.DataContractSerializationRoundTrip(expectedUsingNewValue);


            Assert.Equal(expectedUsingInheritedValue.Test, resultUsingInheritedValue.Test);
            Assert.Equal(expectedUsingNewValue.Test, resultUsingNewValue.Test);
        }
    }
}
