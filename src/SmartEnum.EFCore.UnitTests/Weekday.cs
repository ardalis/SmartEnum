using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnum.EFCore.UnitTests
{
    public sealed class Weekday : SmartEnum<Weekday, int>
    {
        public static readonly Weekday Monday = new Weekday(nameof(Monday), 1);
        public static readonly Weekday Tuesday = new Weekday(nameof(Tuesday), 2);
        public static readonly Weekday Wednesday = new Weekday(nameof(Wednesday), 3);
        public static readonly Weekday Thursday = new Weekday(nameof(Thursday), 4);
        public static readonly Weekday Friday = new Weekday(nameof(Friday), 5);
        public static readonly Weekday Saturday = new Weekday(nameof(Saturday), 6);
        public static readonly Weekday Sunday = new Weekday(nameof(Sunday), 7);

        private Weekday(string name, int value) : base(name, value)
        {
        }
    }
}