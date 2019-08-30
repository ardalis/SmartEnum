using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using SmartEnum.UnitTests;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumWhenThen
    {
        public static TheoryData<SmartFlagTestEnum> NameData =>
            new TheoryData<SmartFlagTestEnum>()
            {
                SmartFlagTestEnum.One,
                SmartFlagTestEnum.Two,
                SmartFlagTestEnum.Three
            };

        [Fact]
        public void DefaultConditionDoesNotRunWhenConditionMet()
        {
            var one = SmartFlagTestEnum.One;

            var firstActionRun = false;
            var defaultActionRun = false;

            one
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .Default(() => defaultActionRun = true);

            firstActionRun.Should().BeTrue();
            defaultActionRun.Should().BeFalse();
        }

        [Fact]
        public void DefaultConditionRunsWhenNoConditionMet()
        {
            var three = SmartFlagTestEnum.Three;

            var firstActionRun = false;
            var secondActionRun = false;
            var defaultActionRun = false;

            three
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .When(SmartFlagTestEnum.Two).Then(() => secondActionRun = true)
                .Default(() => defaultActionRun = true);

            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeFalse();
            defaultActionRun.Should().BeTrue();
        }

        [Fact]
        public void WhenFirstConditionMetFirstActionRuns()
        {
            var one = SmartFlagTestEnum.One;

            var firstActionRun = false;
            var secondActionRun = false;

            one
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .When(SmartFlagTestEnum.Two).Then(() => secondActionRun = true);

            firstActionRun.Should().BeTrue();
            secondActionRun.Should().BeFalse();
        }

        [Fact]
        public void WhenFirstConditionMetSubsequentActionsNotRun()
        {
            var one = SmartFlagTestEnum.One;

            var firstActionRun = false;
            var secondActionRun = false;

            one
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .When(SmartFlagTestEnum.One).Then(() => secondActionRun = true);

            firstActionRun.Should().BeTrue();
            secondActionRun.Should().BeFalse();
        }

        [Fact]
        public void WhenMatchesLastListActionRuns()
        {
            var three = SmartFlagTestEnum.Three;

            var firstActionRun = false;
            var secondActionRun = false;
            var thirdActionRun = false;

            three
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .When(SmartFlagTestEnum.Two).Then(() => secondActionRun = true)
                .When(new List<SmartFlagTestEnum> { SmartFlagTestEnum.One, SmartFlagTestEnum.Two, SmartFlagTestEnum.Three }).Then(() => thirdActionRun = true);

            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeFalse();
            thirdActionRun.Should().BeTrue();
        }

        [Fact]
        public void WhenMatchesLastParameterActionRuns()
        {
            var three = SmartFlagTestEnum.Three;

            var firstActionRun = false;
            var secondActionRun = false;
            var thirdActionRun = false;

            three
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .When(SmartFlagTestEnum.Two).Then(() => secondActionRun = true)
                .When(SmartFlagTestEnum.One, SmartFlagTestEnum.Two, SmartFlagTestEnum.Three).Then(() => thirdActionRun = true);

            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeFalse();
            thirdActionRun.Should().BeTrue();
        }

        [Fact]
        public void WhenSecondConditionMetSecondActionRuns()
        {
            var two = SmartFlagTestEnum.Two;

            var firstActionRun = false;
            var secondActionRun = false;

            two
                .When(SmartFlagTestEnum.One).Then(() => firstActionRun = true)
                .When(SmartFlagTestEnum.Two).Then(() => secondActionRun = true);

            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeTrue();
        }
    }
}
