using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnum.SourceGenerator
{
    internal class VariableInfo
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public VariableInfo(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
