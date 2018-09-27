using Xunit;

namespace SmartEnum.UnitTests.Wcf
{

    public class SmartEnumWcfSerialization
    {
        /// <summary>
        /// Basic test to prove that a class that is derived from <see cref="SmartEnum"/>
        /// can go through the serialization process directly. This test fails
        /// if <see cref="SmartEnum"/> is not marked with the <see cref="DataContract"/>
        ///  attribute and the appropriate <see cref="DataMember"/> attributes.
        /// </summary>
        [Fact]
        public void SurvivesSerializationRoundTrip()
        {
            var result = WcfTestHelper.DataContractSerializationRoundTrip(TestEnum.One);


            Assert.Equal(TestEnum.One, result);
        }

        /// <summary>
        /// Proves that a class that contains a property with a type derived from
        /// <see cref="SmartEnum"/> can go through the serialization process. This
        /// test fails if  <see cref="SmartEnum"/> is not marked with the
        /// <see cref="DataContract"/> attribute and the appropriate
        /// <see cref="DataMember"/> attributes.
        /// </summary>
        [Fact]
        public void SurvivesSerializationRoundTripAsPartOfAnotherObject()
        {
            var expected = new TestObjectWithTestEnumProperty { Test = TestWcfEnum.One };
            
            var result = WcfTestHelper.DataContractSerializationRoundTrip(expected);


            Assert.Equal(expected.Test, result.Test);
        }

        /// <summary>
        /// Proves that a class that contains a <see cref="SmartEnum"/> property
        /// can go through the serialization process when the property is set with
        /// a value that is a derived type of <see cref="SmartEnum"/>. This test
        /// fails if <see cref="SmartEnum"/> does not specify its known types.
        /// </summary>
        [Fact]
        public void SurvivesSerializationRoundTripAsPartOfAnotherObjectAsADerivedType()
        {
            var expected = new TestObjectWithSmartEnumProperty { Test = TestWcfEnum.One };
            
            var result = WcfTestHelper.DataContractSerializationRoundTrip(expected);


            Assert.Equal(expected.Test, result.Test);
        }

        /// <summary>
        /// Proves that a class that contains a <see cref="SmartEnum"/> property
        /// can go through the serialization process when the property is set with
        /// a value that is a derived type of a derived type of <see cref="SmartEnum"/>.
        /// This test fails if <see cref="SmartEnum"/> and its derived type do not
        /// specify known types.
        ///
        /// NOTE: This test, while not specifically for the base <see cref="SmartEnum"/>
        /// functionality, is included as an example to demonstrate expected typical usage.
        /// </summary>
        [Fact]
        public void SurvivesSerializationRoundTripAsPartOfAnotherObjectAsADerivedTypeOfADerivedType()
        {
            var expected = new TestObjectWithSmartEnumProperty { Test = TestWcfEnumSubClass.Four };
            
            var result = WcfTestHelper.DataContractSerializationRoundTrip(expected);


            Assert.Equal(expected.Test, result.Test);
        }
    }
}
