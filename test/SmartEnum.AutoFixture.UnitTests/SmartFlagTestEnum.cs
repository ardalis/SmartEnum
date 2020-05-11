using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;

namespace Ardalis.SmartEnum.AutoFixture.UnitTests
{
    class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
    {
        public static readonly SmartFlagTestEnum One = new SmartFlagTestEnum(nameof(One), 1);
        public static readonly SmartFlagTestEnum Two = new SmartFlagTestEnum(nameof(Two), 2);
        public static readonly SmartFlagTestEnum Three = new SmartFlagTestEnum(nameof(Three), 3);

        public SmartFlagTestEnum(string name, int value) : base(name, value)
        {
        }
    }
}
