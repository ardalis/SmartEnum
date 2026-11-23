# Ardalis.SmartEnum.JsonNet

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

[Json.NET](https://www.newtonsoft.com/json) (Newtonsoft.Json) (de)serialization support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). A SmartEnum should be serialized as either its `Name` or its `Value`, and this package provides converters for both.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.JsonNet
```

## Usage

Apply one of the converters with `[JsonConverter]`. To serialize by `Name`:

```csharp
using Newtonsoft.Json;
using Ardalis.SmartEnum.JsonNet;

public class TestClass
{
    [JsonConverter(typeof(SmartEnumNameConverter<TestEnum, int>))]
    public TestEnum Property { get; set; }
}
```

```json
{ "Property": "One" }
```

To serialize by `Value`:

```csharp
public class TestClass
{
    [JsonConverter(typeof(SmartEnumValueConverter<TestEnum, int>))]
    public TestEnum Property { get; set; }
}
```

```json
{ "Property": 1 }
```

For `SmartFlagEnum` types, use `SmartFlagEnumNameConverter<TEnum, TValue>` or `SmartFlagEnumValueConverter<TEnum, TValue>`.

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum) for full documentation.
