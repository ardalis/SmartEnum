using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.Exceptions;
using Xunit;
using Xunit.Sdk;

namespace SmartFlagEnum.UnitTests
{
    public class SmartTestAttributeSafeContainsPowOfTwo : SmartFlagEnum<SmartTestAttributeSafeContainsPowOfTwo>
    {
        public static readonly SmartTestAttributeSafeContainsPowOfTwo Cheque = new SmartTestAttributeSafeContainsPowOfTwo(nameof(Cheque), 1);
        public static readonly SmartTestAttributeSafeContainsPowOfTwo Card = new SmartTestAttributeSafeContainsPowOfTwo(nameof(Card), 2);
        public static readonly SmartTestAttributeSafeContainsPowOfTwo ChequeAndCard = new SmartTestAttributeSafeContainsPowOfTwo(nameof(ChequeAndCard), 3);
        public static readonly SmartTestAttributeSafeContainsPowOfTwo Cash = new SmartTestAttributeSafeContainsPowOfTwo(nameof(Cash), 4);
        public static readonly SmartTestAttributeSafeContainsPowOfTwo Paypal = new SmartTestAttributeSafeContainsPowOfTwo(nameof(Paypal), 8);
        public static readonly SmartTestAttributeSafeContainsPowOfTwo Explicit = new SmartTestAttributeSafeContainsPowOfTwo(nameof(Explicit), 9);
        public static readonly SmartTestAttributeSafeContainsPowOfTwo ExplicitAboveMax = new SmartTestAttributeSafeContainsPowOfTwo(nameof(ExplicitAboveMax), 17);

        public SmartTestAttributeSafeContainsPowOfTwo(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues : SmartFlagEnum<SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues>
    {
        public static readonly SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues One = new SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues(nameof(One), 1);
        //missing Two
        public static readonly SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues Three = new SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues(nameof(Three), 3);
        public static readonly SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues Four = new SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues(nameof(Four), 4);

        public SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues(string name, int value) : base(name, value)
        {
        }
    }

    [AllowUnsafeFlagEnumValues]
    public class SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues : SmartFlagEnum<SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues>
    {
        public static readonly SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues One = new SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues(nameof(One), 1);
        //Missing Two
        public static readonly SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues Three = new SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues(nameof(Three), 3);
        public static readonly SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues Four = new SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues(nameof(Four), 4);

        public SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartTestAttributePowerOfTwoValuesStartAboveOne : SmartFlagEnum<SmartTestAttributePowerOfTwoValuesStartAboveOne>
    {
        public static readonly SmartTestAttributePowerOfTwoValuesStartAboveOne Four = new SmartTestAttributePowerOfTwoValuesStartAboveOne(nameof(Four), 4);
        public static readonly SmartTestAttributePowerOfTwoValuesStartAboveOne Eight = new SmartTestAttributePowerOfTwoValuesStartAboveOne(nameof(Eight), 8);

        public SmartTestAttributePowerOfTwoValuesStartAboveOne(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo : SmartFlagEnum<SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo>
    {
        public static readonly SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo Four = new SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo(nameof(Four), 4);
        //Missing 8
        public static readonly SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo Sixteen = new SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo(nameof(Sixteen), 16);

        public SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo(string name, int value) : base(name, value)
        {
        }
    }

    public class SmartTestAttributeSinglePowerOfTwoValue : SmartFlagEnum<SmartTestAttributeSinglePowerOfTwoValue>
    {
        public static readonly SmartTestAttributeSinglePowerOfTwoValue Four = new SmartTestAttributeSinglePowerOfTwoValue(nameof(Four), 4);
        public static readonly SmartTestAttributeSinglePowerOfTwoValue Five = new SmartTestAttributeSinglePowerOfTwoValue(nameof(Five), 5);
        public static readonly SmartTestAttributeSinglePowerOfTwoValue Six = new SmartTestAttributeSinglePowerOfTwoValue(nameof(Six), 6);

        public SmartTestAttributeSinglePowerOfTwoValue(string name, int value) : base(name, value)
        {
        }
    }

    public class AllowUnsafeFlagEnumAttributeTests
    {
        [Fact]
        public void DoesNothingIfEnumContainsValidPowerOfTwoValues()
        {
            SmartTestAttributeSafeContainsPowOfTwo.FromValue(2);
        }

        [Fact]
        public void ThrowsExceptionIfEnumDoesNotContainPowerOfTwoValues()
        {
            Assert.Throws<SmartFlagEnumDoesNotContainPowerOfTwoValuesException>(() =>
                SmartTestAttributeSafeDoesNotContainConsecutivePowOfTwoValues.FromValue(1));
        }

        [Fact]
        public void DoesNothingIfEnumDoesNotContainPowerOfTwoValuesAndUnsafeAttributeIsApplied()
        {
            SmartTestAttributeUnsafeDoesNotContainPowOfTwoValues.FromValue(1);
        }

        [Fact]
        public void DoesNothingIfEnumContainsPowerOfTwoValuesStartingAtFour()
        {
            SmartTestAttributePowerOfTwoValuesStartAboveOne.FromValue(4);
        }

        [Fact]
        public void DoesNothingIfEnumContainsSinglePowerOfTwoValue()
        {
            SmartTestAttributeSinglePowerOfTwoValue.FromValue(4);
        }

        [Fact]
        public void ThrowsExceptionIfEnumDoesNotContainConsecutivePowerOfTwoValuesStartingAtFour()
        {
            Assert.Throws<SmartFlagEnumDoesNotContainPowerOfTwoValuesException>(() =>
                SmartTestAttributePowerOfTwoValuesStartAboveOneNotConsecutivePowOfTwo.FromValue(4));
        }
    }
}
