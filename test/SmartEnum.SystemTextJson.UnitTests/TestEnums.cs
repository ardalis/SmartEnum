using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ardalis.SmartEnum.SystemTextJson.UnitTests
{
    public sealed class TestEnumBoolean : SmartEnum<TestEnumBoolean, bool>
    {
        public static readonly TestEnumBoolean Instance = new TestEnumBoolean(nameof(Instance), true);

        TestEnumBoolean(string name, bool value) : base(name, value) { }
    }

    public sealed class TestEnumInt16 : SmartEnum<TestEnumInt16, short>
    {
        public static readonly TestEnumInt16 Instance = new TestEnumInt16(nameof(Instance), 1);

        TestEnumInt16(string name, short value) : base(name, value) { }
    }

    public sealed class TestEnumInt32 : SmartEnum<TestEnumInt32, int>
    {
        public static readonly TestEnumInt32 Instance = new TestEnumInt32(nameof(Instance), 1);

        TestEnumInt32(string name, int value) : base(name, value) { }
    }

    public sealed class TestEnumDouble : SmartEnum<TestEnumDouble, double>
    {
        public static readonly TestEnumDouble Instance = new TestEnumDouble(nameof(Instance), 1.2);

        TestEnumDouble(string name, double value) : base(name, value) { }
    }

    public sealed class TestEnumString : SmartEnum<TestEnumString, string>
    {
        public static readonly TestEnumString Instance = new TestEnumString(nameof(Instance), "1.5");

        TestEnumString(string name, string value) : base(name, value) { }
    }

    public static class TestJsonConverters
    {
        public static readonly JsonSerializerOptions NameConverterOptions = new()
        {
            Converters =
            {
                new SmartEnumNameConverter<TestEnumInt32, int>(),
                new SmartEnumNameConverter<TestEnumString, string>()
            },
            WriteIndented = true
        };

        public static readonly JsonSerializerOptions ValueConverterOptions = new()
        {
            Converters =
            {
                new SmartEnumValueConverter<TestEnumInt32, int>(),
                new SmartEnumValueConverter<TestEnumString, string>()
            },
            WriteIndented = true
        };

    }

    public static class TestDictInt32EnumString
    {
        public static readonly IDictionary<TestEnumInt32, string> Instance = new Dictionary<TestEnumInt32, string>
            { { TestEnumInt32.Instance, nameof(Instance) } };
    }

    public static class TestDictStringEnumString
    {
        public static readonly IDictionary<TestEnumString, string> Instance = new Dictionary<TestEnumString, string>
            { { TestEnumString.Instance, nameof(Instance) } };
    }

    public sealed class DictInt32EnumStringJson
    {
        [JsonPropertyName("1")]
        public string Value => nameof(TestDictInt32EnumString.Instance);
    }

    public sealed class DictStringEnumStringJson
    {
        [JsonPropertyName("1.5")]
        public string Value => nameof(TestDictInt32EnumString.Instance);
    }
}