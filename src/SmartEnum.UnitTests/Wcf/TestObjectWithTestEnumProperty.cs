using System.Runtime.Serialization;
using SmartEnum.UnitTests.Wcf;

namespace SmartEnum.UnitTests
{
    [DataContract]
    public class TestObjectWithTestEnumProperty
    {
        [DataMember]
        public TestWcfEnum Test { get; set; }
    }
}