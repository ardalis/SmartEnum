using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.SmartEnum;
using Xunit;

namespace Ardalis.SmartFlagEnum.UnitTests
{
    public abstract class EmployeeType : SmartFlagEnum<EmployeeType>
    {
        public static readonly EmployeeType Director = new DirectorType();
        public static readonly EmployeeType Manager = new ManagerType();
        public static readonly EmployeeType Assistant = new AssistantType();

        private EmployeeType(string name, int value) : base(name, value)
        {
        }

        public abstract decimal BonusSize { get; }

        private sealed class DirectorType : EmployeeType
        {
            public DirectorType() : base("Director", 1) { }

            public override decimal BonusSize => 100_000m;
        }

        private sealed class ManagerType : EmployeeType
        {
            public ManagerType() : base("Manager", 2) { }

            public override decimal BonusSize => 10_000m;
        }

        private sealed class AssistantType : EmployeeType
        {
            public AssistantType() : base("Assistant", 4) { }

            public override decimal BonusSize => 1_000m;
        }
    }

    public class SmartFlagEnumUsageExample
    {
        [Fact]
        public void UseSmartFlagEnum()
        {
            var result = EmployeeType.FromValue(3).ToList();

            var outputString = "";
            foreach (var employeeType in result)
            {
                outputString += $"{employeeType.Name} earns ${employeeType.BonusSize} bonus this year.\n";
            }

            Assert.Equal("Director earns $100000 bonus this year.\n" + "Manager earns $10000 bonus this year.\n",
                outputString);
            
            var allResult = EmployeeType.FromValueToString(-1);
            Assert.Equal("Director, Manager, Assistant", allResult);

            var orResult = EmployeeType.FromValueToString(EmployeeType.Assistant | EmployeeType.Director);
            Assert.Equal("Director, Assistant", orResult);
        }
    }
}
