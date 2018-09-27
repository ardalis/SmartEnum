using System.Runtime.Serialization;

namespace SmartEnum.UnitTests.Wcf
{
    [DataContract]
    public class TestObjectWithTestEnumProperty
    {
        [DataMember]
        public TestWcfEnum Test { get; set; }
    }
}