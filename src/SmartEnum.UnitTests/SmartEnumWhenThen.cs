namespace Ardalis.SmartEnum.UnitTests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class SmartEnumWhenThen
    {
        public static TheoryData<TestEnum> NameData =>
            new TheoryData<TestEnum> 
            {
                TestEnum.One,
                TestEnum.Two,
                TestEnum.Three, 
            };

        [Fact]
        public void WhenFirstConditionMetFirstActionRuns()
        {
            var one = TestEnum.One;

            var firstActionRun = false;
            var secondActionRun = false;

            one
                .When(TestEnum.One).Then(() => firstActionRun = true)
                .When(TestEnum.Two).Then(() => secondActionRun = true);


            firstActionRun.Should().BeTrue();
            secondActionRun.Should().BeFalse();
        }

        [Fact]
        public void WhenMatchesLastListActionRuns()
        {
            var three = TestEnum.Three;

            var firstActionRun = false;
            var secondActionRun = false;
            var thirdActionRun = false;

            three
                .When(TestEnum.One).Then(() => firstActionRun = true)
                .When(TestEnum.Two).Then(() => secondActionRun = true)
                .When(new List<TestEnum> { TestEnum.One, TestEnum.Two, TestEnum.Three }).Then(() => thirdActionRun = true);


            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeFalse();
            thirdActionRun.Should().BeTrue();
        }

        [Fact]
        public void WhenMatchesLastParameterActionRuns()
        {
            var three = TestEnum.Three;

            var firstActionRun = false;
            var secondActionRun = false;
            var thirdActionRun = false;

            three
                .When(TestEnum.One).Then(() => firstActionRun = true)
                .When(TestEnum.Two).Then(() => secondActionRun = true)
                .When(TestEnum.One, TestEnum.Two, TestEnum.Three).Then(() => thirdActionRun = true);


            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeFalse();
            thirdActionRun.Should().BeTrue();
        }

        [Fact]
        public void WhenSecondConditionMetSecondActionRuns()
        {
            var two = TestEnum.Two;

            var firstActionRun = false;
            var secondActionRun = false;

            two
                .When(TestEnum.One).Then(() => firstActionRun = true)
                .When(TestEnum.Two).Then(() => secondActionRun = true);


            firstActionRun.Should().BeFalse();
            secondActionRun.Should().BeTrue();
        }
    }
}
