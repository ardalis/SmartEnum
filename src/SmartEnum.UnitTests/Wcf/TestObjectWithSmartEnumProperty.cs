using System.Runtime.Serialization;
using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests.Wcf
{
    [DataContract]
    public class TestObjectWithSmartEnumProperty
    {
        [DataMember]
        public SmartEnum<TestWcfEnum, int> Test { get; set; }
    }
}