using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;

namespace Ardalis.SmartEnum.UnitTests
{
    public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
    {
        public static readonly SmartFlagTestEnum Zero = new SmartFlagTestEnum(nameof(Zero), 0);
        public static readonly SmartFlagTestEnum One = new SmartFlagTestEnum(nameof(One), 1);
        public static readonly SmartFlagTestEnum Two = new SmartFlagTestEnum(nameof(Two), 2);
        public static readonly SmartFlagTestEnum Three = new SmartFlagTestEnum(nameof(Three), 4);
        public static readonly SmartFlagTestEnum Four = new SmartFlagTestEnum(nameof(Four), 8);
        public static readonly SmartFlagTestEnum Five = new SmartFlagTestEnum(nameof(Five), 16);

        public SmartFlagTestEnum(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartFlagExplicitDeclaredCombinationTestEnum : SmartFlagEnum<SmartFlagExplicitDeclaredCombinationTestEnum>
    {
        public static readonly SmartFlagExplicitDeclaredCombinationTestEnum Zero = new SmartFlagExplicitDeclaredCombinationTestEnum(nameof(Zero), 0);
        public static readonly SmartFlagExplicitDeclaredCombinationTestEnum One = new SmartFlagExplicitDeclaredCombinationTestEnum(nameof(One), 1);
        public static readonly SmartFlagExplicitDeclaredCombinationTestEnum Two = new SmartFlagExplicitDeclaredCombinationTestEnum(nameof(Two), 2);
        public static readonly SmartFlagExplicitDeclaredCombinationTestEnum Three = new SmartFlagExplicitDeclaredCombinationTestEnum(nameof(Three), 3);
        public static readonly SmartFlagExplicitDeclaredCombinationTestEnum Five = new SmartFlagExplicitDeclaredCombinationTestEnum(nameof(Five), 5);
        
        public SmartFlagExplicitDeclaredCombinationTestEnum(string name, int value) : base(name, value)
        {
        }
    }

    public class FlagEnumDecimal : SmartFlagEnum<FlagEnumDecimal, decimal>
    {
        public static readonly FlagEnumDecimal Zero = new FlagEnumDecimal(nameof(Zero), 0M);
        public static readonly FlagEnumDecimal One = new FlagEnumDecimal(nameof(One), 1M);
        public static readonly FlagEnumDecimal Two = new FlagEnumDecimal(nameof(Two), 2M);
        public static readonly FlagEnumDecimal Three = new FlagEnumDecimal(nameof(Three), 4M);
        public static readonly FlagEnumDecimal Four = new FlagEnumDecimal(nameof(Four), 8M);
        public static readonly FlagEnumDecimal Five = new FlagEnumDecimal(nameof(Five), 16M);

        public FlagEnumDecimal(string name, decimal value) : base(name, value)
        {
        }
    }

    public class SmartFlagTestEnumV2 : SmartFlagEnum<SmartFlagTestEnumV2>
    {
        public static readonly SmartFlagTestEnumV2 One = new SmartFlagTestEnumV2(nameof(One), 1);
        public static readonly SmartFlagTestEnumV2 Two = new SmartFlagTestEnumV2(nameof(Two), 2);
        public static readonly SmartFlagTestEnumV2 Three = new SmartFlagTestEnumV2(nameof(Three), 4);
        public static readonly SmartFlagTestEnumV2 Four = new SmartFlagTestEnumV2(nameof(Four), 8);

        public SmartFlagTestEnumV2(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartFlagNotPowerOfTwoTestEnum : SmartFlagEnum<SmartFlagNotPowerOfTwoTestEnum>
    {
        public static readonly SmartFlagNotPowerOfTwoTestEnum Zero = new SmartFlagNotPowerOfTwoTestEnum(nameof(Zero), 0);
        public static readonly SmartFlagNotPowerOfTwoTestEnum One = new SmartFlagNotPowerOfTwoTestEnum(nameof(One), 3);
        public static readonly SmartFlagNotPowerOfTwoTestEnum Two = new SmartFlagNotPowerOfTwoTestEnum(nameof(Two), 4);
        public static readonly SmartFlagNotPowerOfTwoTestEnum Three = new SmartFlagNotPowerOfTwoTestEnum(nameof(Three), 7);
        public static readonly SmartFlagNotPowerOfTwoTestEnum Four = new SmartFlagNotPowerOfTwoTestEnum(nameof(Four), 9);
        public static readonly SmartFlagNotPowerOfTwoTestEnum Five = new SmartFlagNotPowerOfTwoTestEnum(nameof(Five), 15);

        public SmartFlagNotPowerOfTwoTestEnum(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartFlagNegativeTestEnum : SmartFlagEnum<SmartFlagNegativeTestEnum>
    {
        public static readonly SmartFlagNegativeTestEnum One = new SmartFlagNegativeTestEnum(nameof(One), 1);
        public static readonly SmartFlagNegativeTestEnum Two = new SmartFlagNegativeTestEnum(nameof(Two), -2);

        public SmartFlagNegativeTestEnum(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartFlagNegativeTestEnumV2 : SmartFlagEnum<SmartFlagNegativeTestEnumV2>
    {
        public static readonly SmartFlagNegativeTestEnumV2 Negative = new SmartFlagNegativeTestEnumV2(nameof(Negative), -34);
        public static readonly SmartFlagNegativeTestEnumV2 One = new SmartFlagNegativeTestEnumV2(nameof(One), 1);
        public static readonly SmartFlagNegativeTestEnumV2 Two = new SmartFlagNegativeTestEnumV2(nameof(Two), 2);
        public static readonly SmartFlagNegativeTestEnumV2 Three = new SmartFlagNegativeTestEnumV2(nameof(Three), 4);

        public SmartFlagNegativeTestEnumV2(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartFlagNegativeAndZeroMultiValueTestEnum : SmartFlagEnum<SmartFlagNegativeAndZeroMultiValueTestEnum>
    {
        public static readonly SmartFlagNegativeAndZeroMultiValueTestEnum All = new SmartFlagNegativeAndZeroMultiValueTestEnum(nameof(All), -1);
        public static readonly SmartFlagNegativeAndZeroMultiValueTestEnum None = new SmartFlagNegativeAndZeroMultiValueTestEnum(nameof(None), 0);
        public static readonly SmartFlagNegativeAndZeroMultiValueTestEnum One = new SmartFlagNegativeAndZeroMultiValueTestEnum(nameof(One), 1);
        public static readonly SmartFlagNegativeAndZeroMultiValueTestEnum Two = new SmartFlagNegativeAndZeroMultiValueTestEnum(nameof(Two), 2);

        public SmartFlagNegativeAndZeroMultiValueTestEnum(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartFlagTestStringEnum : SmartFlagEnum<SmartFlagTestStringEnum, string>
    {
        public static readonly SmartFlagTestStringEnum One = new SmartFlagTestStringEnum(nameof(One), nameof(One));
        public static readonly SmartFlagTestStringEnum Two = new SmartFlagTestStringEnum(nameof(Two), nameof(Two));
        public static readonly SmartFlagTestStringEnum Three = new SmartFlagTestStringEnum(nameof(Three), nameof(Three));

        public SmartFlagTestStringEnum(string name, string value) : base(name, value)
        {
        }
    }

    public class SmartFlagAlternativeValueNotation : SmartFlagEnum<SmartFlagAlternativeValueNotation>
    {
        public static readonly SmartFlagAlternativeValueNotation None = new SmartFlagAlternativeValueNotation(nameof(None), 0);
        public static readonly SmartFlagAlternativeValueNotation One = new SmartFlagAlternativeValueNotation(nameof(One), 1 << 0);
        public static readonly SmartFlagAlternativeValueNotation Two = new SmartFlagAlternativeValueNotation(nameof(Two), 1 << 1);
        public static readonly SmartFlagAlternativeValueNotation Three = new SmartFlagAlternativeValueNotation(nameof(Three), 1 << 2);

        public SmartFlagAlternativeValueNotation(string name, int value) : base(name, value)
        {
        }
    }
}
