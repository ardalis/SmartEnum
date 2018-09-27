using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests.Wcf
{
    [DataContract]
    [KnownType(nameof(GetKnownTypes))]
    public class TestWcfEnum : SmartEnum<TestWcfEnum, int>
    {
        public static TestWcfEnum One = new TestWcfEnum(nameof(One), 1);
        public static TestWcfEnum Two = new TestWcfEnum(nameof(Two), 2);
        public static TestWcfEnum Three = new TestWcfEnum(nameof(Three), 3);

        protected TestWcfEnum(string name, int value) : base(name, value)
        {
        }

        protected TestWcfEnum() : base()
        {
            // required for EF
        }

        private static IEnumerable<Type> _knownTypes;

        /// <summary>
        /// Finds all subclasses of this type via reflection so they can be
        /// known to the serializer.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> GetKnownTypes()
        {
            // The list has already been created, no need to do it again.
            if (_knownTypes != null) return _knownTypes;

            _knownTypes = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(assembly => assembly.GetTypes())
                            .Where(type => type.IsSubclassOf(typeof(TestWcfEnum)));

            return _knownTypes;
        }
    }
}
