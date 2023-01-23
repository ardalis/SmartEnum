using Ardalis.SmartEnum;

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnum.SourceGenerator.UnitTests
{
    [SmartEnumGenerator]
    public sealed partial class Subscriptions : SmartEnum<Subscriptions>
    {
        public static readonly Subscriptions Free;
        public static readonly Subscriptions Sliver;
        public static readonly Subscriptions Gold;

        public Subscriptions(string name, int value) : base(name, value)
        {
        }
    }
}
