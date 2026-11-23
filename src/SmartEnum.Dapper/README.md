# Ardalis.SmartEnum.Dapper

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

[Dapper](https://github.com/DapperLib/Dapper) support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). Maps SmartEnum properties to and from a database column using either the enum's `Name` or its `Value`.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.Dapper
```

## Usage

Register a type handler for each SmartEnum type:

```csharp
using Ardalis.SmartEnum.Dapper;
using Dapper;

// Store the Name (e.g. "One", "Two", "Three")
SqlMapper.AddTypeHandler(typeof(TestEnum), new SmartEnumByNameTypeHandler<TestEnum>());

// ...or store the Value (e.g. 1, 2, 3)
SqlMapper.AddTypeHandler(typeof(TestEnum), new SmartEnumByValueTypeHandler<TestEnum>());
```

To skip explicit registration, inherit from `DapperSmartEnumByName` or `DapperSmartEnumByValue` instead of `SmartEnum`:

```csharp
public class TestEnumByName : DapperSmartEnumByName<TestEnumByName>
{
    public static readonly TestEnumByName One = new TestEnumByName(1);
    public static readonly TestEnumByName Two = new TestEnumByName(2);

    protected TestEnumByName(int value, [CallerMemberName] string name = null) : base(name, value)
    {
    }
}
```

These types can be decorated with `[DbType(...)]`, `[DoNotSetDbType]`, or (for by-name enums) `[IgnoreCase]` to configure their type handler.

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum#dapper-support) for full documentation.
