[![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum)
![Last Publish Ardalis.SmartEnum to NuGet](https://github.com/ardalis/SmartEnum/workflows/publish%20Ardalis.SmartEnum%20to%20nuget/badge.svg)

## Announcement!

Ardalis.SmartEnum is moving to a commercial license. [Tell me more](https://nimbletools.io/news/ardalis-nuget-packages/)

## Table Of Contents

- [Sub-packages](#sub-packages)
- [Give a Star! ⭐](#give-a-star-star)
- [SmartEnum](#smart-enum)
  * [Contributors](#contributors)
- [Install](#install)
  * [Version](#version)
- [Usage](#usage)
  * [List](#list)
  * [FromName()](#fromname)
  * [FromValue()](#fromvalue)
  * [ToString()](#tostring)
  * [Switch](#switch)
  * [SmartFlagEnum](#smartflagenum)
  * [Setting SmartFlagEnum Values](#setting-smartflagenum-values)
  * [Usage - (SmartFlagEnum)](#usage---smartflagenum)
  * [FromName()](#fromname-1)
  * [FromValue()](#fromvalue-1)
  * [FromValueToString()](#fromvaluetostring)
  * [BitWiseOrOperator](#bitwiseoroperator)
  * [Persisting with EF Core 2.1 or higher](#persisting-with-ef-core-21-or-higher)
  * [Using SmartEnum.EFCore](#using-smartenumefcore)
  * [AutoFixture support](#autofixture-support)
  * [Guard Clauses support](#guard-clauses-support)
  * [Json support](#json-support)
  * [Model Binding support](#model-binding-support)
  * [Dapper support](#dapper-support)
  * [DapperSmartEnum](#dappersmartenum)
  * [Case Insensitive String Enum](#case-insensitive-string-enum)
  * [Name Validation Attribute](#name-validation-attribute)
  * [Examples in the Real World](#examples-in-the-real-world)
  * [References](#references)

### Sub-packages

SmartEnum.AutoFixture: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.AutoFixture.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.AutoFixture)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.AutoFixture.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.AutoFixture)![publish SmartEnum.AutoFixture to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.AutoFixture%20to%20nuget/badge.svg)

SmartEnum.Dapper: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.Dapper.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.Dapper)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.Dapper.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.Dapper)![publish SmartEnum.Dapper to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.Dapper%20to%20nuget/badge.svg)

SmartEnum.EFCore: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.EFCore.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.EFCore)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.EFCore.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.EFCore)![publish SmartEnum.EFCore to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.EFCore%20to%20nuget/badge.svg)

SmartEnum.GuardClauses: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.GuardClauses)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.GuardClauses)![publish SmartEnum.GuardClauses to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.GuardClauses%20to%20nuget/badge.svg)

SmartEnum.JsonNet: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.JsonNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.JsonNet)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.JsonNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.JsonNet)![publish jsonnet to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.JsonNet%20to%20nuget/badge.svg)

SmartEnum.MessagePack: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.MessagePack.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.MessagePack)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.MessagePack.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.MessagePack)![publish SmartEnum.MessagePack to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.MessagePack%20to%20nuget/badge.svg)

SmartEnum.ModelBinding: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.ModelBinding.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.ModelBinding)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.ModelBinding.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.ModelBinding)![publish Ardalis.SmartEnum.ModelBinding to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20Ardalis.SmartEnum.ModelBinding%20to%20nuget/badge.svg)

SmartEnum.ProtoBufNet: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.ProtoBufNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.ProtoBufNet)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.ProtoBufNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.ProtoBufNet)![publish SmartEnum.ProtoBufNet to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.ProtoBufNet%20to%20nuget/badge.svg)

SmartEnum.SystemTextJson: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.SystemTextJson.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.SystemTextJson)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.SystemTextJson.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.SystemTextJson)![publish SmartEnum.SystemTextJson to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.SystemTextJson%20to%20nuget/badge.svg)

SmartEnum.Utf8Json: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.Utf8Json.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.Utf8Json)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.Utf8Json.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.Utf8Json)![publish SmartEnum.Utf8Json to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.Utf8Json%20to%20nuget/badge.svg)

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

# Smart Enum

An implementation of a [type-safe object-oriented alternative](https://codeblog.jonskeet.uk/2006/01/05/classenum/) to [C# enum](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum).

## Contributors

Thanks to [Scott DePouw](https://github.com/sdepouw), [Antão Almada](https://github.com/aalmada), and [Nagasudhir Pulla](https://github.com/nagasudhirpulla) for help with this project!

# Install

The framework is provided as a set of NuGet packages. In many cases you'll only need the base package, but if you need serialization and/or ORM support there are many implementation-specific packages available to assist.

To install the minimum requirements:

```
Install-Package Ardalis.SmartEnum
```

To install support for serialization, AutoFixture, EF Core, Model Binding, Guard Clauses, or Dapper, select the lines that apply:

```
Install-Package Ardalis.SmartEnum.AutoFixture
Install-Package Ardalis.SmartEnum.JsonNet
Install-Package Ardalis.SmartEnum.SystemTextJson
Install-Package Ardalis.SmartEnum.Utf8Json
Install-Package Ardalis.SmartEnum.MessagePack
Install-Package Ardalis.SmartEnum.ProtoBufNet
Install-Package Ardalis.SmartEnum.EFCore
Install-Package Ardalis.SmartEnum.ModelBinding
Install-Package Ardalis.SmartEnum.Dapper
Install-Package Ardalis.SmartEnum.GuardClauses
```

## Version

The latest version of the package supports .NET 6, .NET 7, .NET 8, and .NET Standard 2.0. (`Ardalis.SmartEnum.EFCore` targets .NET 6, 7, and 8 only.)

## Usage

Define your smart enum by inheriting from `SmartEnum<TEnum>` where `TEnum` is the type you're declaring. For [example](/test/SmartEnum.UnitTests/TestEnum.cs):

```csharp
using Ardalis.SmartEnum;

public sealed class TestEnum : SmartEnum<TestEnum>
{
    public static readonly TestEnum One = new TestEnum(nameof(One), 1);
    public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
    public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);

    private TestEnum(string name, int value) : base(name, value)
    {
    }
}
```

The default value type is `int` but it can be set using the second generic argument `TValue`.
The string alias can also be set explicitly, where spaces are allowed.

```csharp
using Ardalis.SmartEnum;

public sealed class TestEnum : SmartEnum<TestEnum, ushort>
{
    public static readonly TestEnum One = new TestEnum("A string!", 1);
    public static readonly TestEnum Two = new TestEnum("Another string!", 2);
    public static readonly TestEnum Three = new TestEnum("Yet another string!", 3);

    private TestEnum(string name, ushort value) : base(name, value)
    {
    }
}
```

Just like regular `enum`, more than one string can be assigned to the same value but only one value can be assigned to a string:

```csharp
using Ardalis.SmartEnum;

public sealed class TestEnum : SmartEnum<TestEnum>
{
    public static readonly TestEnum One = new TestEnum(nameof(One), 1);
    public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
    public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);
    public static readonly TestEnum AnotherThree = new TestEnum(nameof(AnotherThree), 3);
    // public static readonly TestEnum ThreeAgain = new TestEnum(nameof(Three), 4); -> duplicate name, throws on first name lookup

    private TestEnum(string name, int value) : base(name, value)
    {
    }
}
```

In this case, `TestEnum.FromValue(3)` will return the first instance found, either `TestEnum.Three` or `TestEnum.AnotherThree`. No order should be assumed.

The `Value` content is used when comparing two smart enums, while `Name` is ignored:

```csharp
TestEnum.One.Equals(TestEnum.One); // returns true
TestEnum.One.Equals(TestEnum.Three); // returns false
TestEnum.Three.Equals(TestEnum.AnotherThree); // returns true
```

Inheritance can be used to add "behavior" to a smart enum.

This example adds a `BonusSize` property, avoiding the use of the `switch` typically used with regular enums:

```csharp
using Ardalis.SmartEnum;

public abstract class EmployeeType : SmartEnum<EmployeeType>
{
    public static readonly EmployeeType Manager = new ManagerType();
    public static readonly EmployeeType Assistant = new AssistantType();

    private EmployeeType(string name, int value) : base(name, value)
    {
    }

    public abstract decimal BonusSize { get; }

    private sealed class ManagerType : EmployeeType
    {
        public ManagerType() : base("Manager", 1) {}

        public override decimal BonusSize => 10_000m;
    }

    private sealed class AssistantType : EmployeeType
    {
        public AssistantType() : base("Assistant", 2) {}

        public override decimal BonusSize => 1_000m;
    }
}
```

You can take this a step further and use `EmployeeType` and its `BonusSize` property from another class like so:

```csharp
public class Employee
{
    private EmployeeType _employeeType = EmployeeType.Manager;
    public string Type
    {
        get => _employeeType.Name;
        set
        {
            if (!EmployeeType.TryFromName(value, true, out var parsed))
            {
                throw new Exception($"Invalid employee type of '{value}'");
            }
            _employeeType = parsed;
        }
    }

    public decimal BonusSize => _employeeType.BonusSize;
}
```

This other example implements a *state machine*. The method `CanTransitionTo()` returns `true` if it's allowed to transition from current state to `next`; otherwise returns `false`.

```csharp
using Ardalis.SmartEnum;

public abstract class ReservationStatus : SmartEnum<ReservationStatus>
{
    public static readonly ReservationStatus New = new NewStatus();
    public static readonly ReservationStatus Accepted = new AcceptedStatus();
    public static readonly ReservationStatus Paid = new PaidStatus();
    public static readonly ReservationStatus Cancelled = new CancelledStatus();

    private ReservationStatus(string name, int value) : base(name, value)
    {
    }

    public abstract bool CanTransitionTo(ReservationStatus next);

    private sealed class NewStatus: ReservationStatus
    {
        public NewStatus() : base("New", 0)
        {
        }

        public override bool CanTransitionTo(ReservationStatus next) =>
            next == ReservationStatus.Accepted || next == ReservationStatus.Cancelled;
    }

    private sealed class AcceptedStatus: ReservationStatus
    {
        public AcceptedStatus() : base("Accepted", 1)
        {
        }

        public override bool CanTransitionTo(ReservationStatus next) =>
            next == ReservationStatus.Paid || next == ReservationStatus.Cancelled;
    }

    private sealed class PaidStatus: ReservationStatus
    {
        public PaidStatus() : base("Paid", 2)
        {
        }

        public override bool CanTransitionTo(ReservationStatus next) =>
            next == ReservationStatus.Cancelled;
    }

    private sealed class CancelledStatus: ReservationStatus
    {
        public CancelledStatus() : base("Cancelled", 3)
        {
        }

        public override bool CanTransitionTo(ReservationStatus next) =>
            false;
    }
}
```

### List

You can list all of the available options using the enum's static `List` property:

```csharp
foreach (var option in TestEnum.List)
    Console.WriteLine(option.Name);
```

`List` returns an `IReadOnlyCollection` so you can use the `Count` property to efficiently get the number of available options.

```csharp
var count = TestEnum.List.Count;
```

### FromName()

Access an instance of an enum by matching a string to its `Name` property:

```csharp
var myEnum = TestEnum.FromName("One");
```

Exception `SmartEnumNotFoundException` is thrown when name is not found. Alternatively, you can use `TryFromName` that returns `false` when name is not found:

```csharp
if (TestEnum.TryFromName("One", out var myEnum))
{
    // use myEnum here
}
```

Both methods have a `ignoreCase` parameter (the default is case sensitive).

### FromValue()

Access an instance of an enum by matching its value:

```csharp
var myEnum = TestEnum.FromValue(1);
```

Exception `SmartEnumNotFoundException` is thrown when value is not found. Alternatively, you can use `TryFromValue` that returns `false` when value is not found:

```csharp
if (TestEnum.TryFromValue(1, out var myEnum))
{
    // use myEnum here
}
```

### ToString()

Display an enum using the `ToString()` override:

```csharp
Console.WriteLine(TestEnum.One); // One
```

### Switch

Given an instance of a TestEnum, switch depending on its name:

```csharp
switch(testEnumVar.Name)
{
    case nameof(TestEnum.One):
        ...
        break;
    case nameof(TestEnum.Two):
        ...
        break;
    case nameof(TestEnum.Three):
        ...
        break;
    default:
        ...
        break;
}
```

Using pattern matching:

```csharp
switch(testEnumVar)
{
    case null:
        ...
        break;
    case var e when e.Equals(TestEnum.One):
        ...
        break;
    case var e when e.Equals(TestEnum.Two):
        ...
        break;
    case var e when e.Equals(TestEnum.Three):
        ...
        break;
    default:
        ...
        break;
}
```

Because of the limitations of pattern matching SmartEnum also provides a fluent interface to help create clean code:

```csharp
testEnumVar
    .When(TestEnum.One).Then(() => ... )
    .When(TestEnum.Two).Then(() => ... )
    .When(TestEnum.Three).Then(() => ... )
    .Default( ... );
```

N.B. For performance critical code the fluent interface carries some overhead that you may wish to avoid. See the available [benchmarks](benchmarks/SmartEnum.Benchmarks) code for your use case.

### SmartFlagEnum

Support has been added for a `Flag` functionality.
This feature is similar to the behaviour seen when applying the `[Flags]` attribute to Enums in the .NET Framework.
All methods available on the `SmartFlagEnum` class return an `IEnumerable<SmartFlagEnum>` with one or more values depending on the value provided/method called.
Some functionality is shared with the original SmartEnum class, listed below are the variations.

### Setting SmartFlagEnum Values

When setting the values for a `SmartFlagEnum` It is imperative to provide values as powers of two.  If at least one value is not set as power of two or two or more power of two values are provided inconsecutively (eg: 1, 2, no four!, 8) a `SmartFlagEnumDoesNotContainPowerOfTwoValuesException` will be thrown.

```csharp
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
{
    public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
    public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);
    public static readonly SmartFlagTestEnum Cash = new SmartFlagTestEnum(nameof(Cash), 2);
    public static readonly SmartFlagTestEnum Bpay = new SmartFlagTestEnum(nameof(Bpay), 4);
    public static readonly SmartFlagTestEnum Paypal = new SmartFlagTestEnum(nameof(Paypal), 8);
    public static readonly SmartFlagTestEnum BankTransfer = new SmartFlagTestEnum(nameof(BankTransfer), 16);

    public SmartFlagTestEnum(string name, int value) : base(name, value)
    {
    }
}
```

This behaviour can be disabled by applying the `AllowUnsafeFlagEnumValuesAttribute` to the smart enum class.  Note: If power of two values are not provided the SmartFlagEnum will not behave as expected!

```csharp
[AllowUnsafeFlagEnumValues]
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
{
    public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
    public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);
    public static readonly SmartFlagTestEnum Cash = new SmartFlagTestEnum(nameof(Cash), 2);
    public static readonly SmartFlagTestEnum Bpay = new SmartFlagTestEnum(nameof(Bpay), 4);
    public static readonly SmartFlagTestEnum Paypal = new SmartFlagTestEnum(nameof(Paypal), 8);
    public static readonly SmartFlagTestEnum BankTransfer = new SmartFlagTestEnum(nameof(BankTransfer), 16);

    public SmartFlagTestEnum(string name, int value) : base(name, value)
    {
    }
}
```

`Combination` values can be provided explicitly and will be returned in place of the multiple flag values that would have been returned from the `FromValue()` method.

```csharp
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
{
    public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
    public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);
    public static readonly SmartFlagTestEnum Cash = new SmartFlagTestEnum(nameof(Cash), 2);
    public static readonly SmartFlagTestEnum CardAndCash = new SmartFlagTestEnum(nameof(CardAndCash), 3); // Explicit `Combination` value
    public static readonly SmartFlagTestEnum Bpay = new SmartFlagTestEnum(nameof(Bpay), 4);
    public static readonly SmartFlagTestEnum Paypal = new SmartFlagTestEnum(nameof(Paypal), 8);
    public static readonly SmartFlagTestEnum BankTransfer = new SmartFlagTestEnum(nameof(BankTransfer), 16);

    public SmartFlagTestEnum(string name, int value) : base(name, value)
    {
    }
}
```

These explicit values can be provided above the highest allowable flag value without consequence, however attempting to access a value that is higher than the maximum flag value that has not explicitly been provided (for example 4) will cause a `SmartEnumNotFoundException` to be thrown.

```csharp
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
{
    public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
    public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);
    public static readonly SmartFlagTestEnum Cash = new SmartFlagTestEnum(nameof(Cash), 2);
    public static readonly SmartFlagTestEnum AfterPay = new SmartFlagTestEnum(nameof(AfterPay), 5);

    public SmartFlagTestEnum(string name, int value) : base(name, value)
    {
    }
}

SmartFlagTestEnum.FromValue(3); // Works!
SmartFlagTestEnum.FromValue(5); // Works!
SmartFlagTestEnum.FromValue(4); // Throws SmartEnumNotFoundException
```

A Negative One (-1) value may be provided as an `All` value. When a value of -1 is passed into any of the `FromValue()` methods an IEnumerable containing all values (excluding 0) will be returned.
If an explicit `Combination` value exists with a value of -1 this will be returned instead.

```csharp
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
{
    public static readonly SmartFlagTestEnum All = new SmartFlagTestEnum(nameof(All), -1);
    public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
    public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);
    public static readonly SmartFlagTestEnum Cash = new SmartFlagTestEnum(nameof(Cash), 2);
    public static readonly SmartFlagTestEnum Bpay = new SmartFlagTestEnum(nameof(Bpay), 4);
    public static readonly SmartFlagTestEnum Paypal = new SmartFlagTestEnum(nameof(Paypal), 8);
    public static readonly SmartFlagTestEnum BankTransfer = new SmartFlagTestEnum(nameof(BankTransfer), 16);

    public SmartFlagTestEnum(string name, int value) : base(name, value)
    {
    }
}
```

### Usage - (SmartFlagEnum)

```csharp
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
    public void UseSmartFlagEnumOne()
    {
        var result = EmployeeType.FromValue(3).ToList();

        var outputString = "";
        foreach (var employeeType in result)
        {
            outputString += $"{employeeType.Name} earns ${employeeType.BonusSize} bonus this year.\n";
        }

        // outputString == "Director earns $100000 bonus this year.\nManager earns $10000 bonus this year.\n"
    }

    public void UseSmartFlagEnumTwo()
    {
        var names = EmployeeType.FromValueToString(-1);
        // names == "Director, Manager, Assistant"
    }

    public void UseSmartFlagEnumThree()
    {
        var names = EmployeeType.FromValueToString(EmployeeType.Assistant | EmployeeType.Director);
        // names == "Director, Assistant"
    }
}
```

### FromName()

Access an `IEnumerable` of enum instances by matching a string containing one or more enum names separated by commas to its `Names` property:

```csharp
var myFlagEnums = TestFlagEnum.FromName("One, Two");
```

Exception `SmartEnumNotFoundException` is thrown when no names are found. Alternatively, you can use `TryFromName` that returns `false` when no names are found:

```csharp
if (TestFlagEnum.TryFromName("One, Two", out var myFlagEnums))
{
    // use myFlagEnums here
}
```

Both methods have a `ignoreCase` parameter (the default is case sensitive).

### FromValue()

Access an `IEnumerable` of enum instances by matching a value:

```csharp
var myFlagEnums = TestFlagEnum.FromValue(3);
```

Exception `SmartEnumNotFoundException` is thrown when no values are found. Alternatively, you can use `TryFromValue` that returns `false` when values are not found:

```csharp
if (TestFlagEnum.TryFromValue(3, out var myFlagEnums))
{
    // use myFlagEnums here
}
```

Note: Negative values other than (-1) passed into this method will cause a `NegativeValueArgumentException` to be thrown, this behaviour can be disabled by applying the `AllowNegativeInputValues` attribute to the desired `SmartFlagEnum` class.

```csharp
[AllowNegativeInputValues]
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
{
    public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
    public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);

    public SmartFlagTestEnum(string name, int value) : base(name, value)
    {
    }
}
```

Note: `FromValue()` will accept any input that can be successfully parsed as an integer.  If an invalid value is supplied it will throw an `InvalidFlagEnumValueParseException`.

### FromValueToString()

Return a comma-separated string of the names of the matching enum instances:

```csharp
var myFlagEnumString = TestFlagEnum.FromValueToString(3);
```

Exception `SmartEnumNotFoundException` is thrown when no values are found. Alternatively, you can use `TryFromValueToString` that returns `false` when values are not found:

```csharp
if (TestFlagEnum.TryFromValueToString(3, out var myFlagEnumsAsString))
{
    // use myFlagEnumsAsString here
}
```

Note: Negative values other than (-1) passed into this method will cause a `NegativeValueArgumentException` to be thrown, this behaviour can be disabled by applying the `AllowNegativeInputValues` attribute to the desired `SmartFlagEnum` class.

### BitWiseOrOperator

The `FromValue()` methods allow the Or ( | ) operator to be used to `add` enum values together and provide multiple values at once.

```csharp
var myFlagEnums = TestFlagEnum.FromValue(TestFlagEnum.One | TestFlagEnum.Two);
```

This will only work where the type of the `SmartFlagEnum` has been specified as `Int32` or else can be explicitly cast as an `Int32`.

```csharp
var myFlagEnums = TestFlagEnumDecimal.FromValue((int)TestFlagEnumDecimal.One | (int)TestFlagEnumDecimal.Two);
```

### Persisting with EF Core 2.1 or higher

EF Core 2.1 introduced [value conversions](https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions) which can be used to map SmartEnum types to simple database types. For example, given an entity named `Policy` with a property `PolicyStatus` that is a SmartEnum, you could use the following code to persist just the value to the database:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<Policy>()
        .Property(p => p.PolicyStatus)
        .HasConversion(
            p => p.Value,
            p => PolicyStatus.FromValue(p));
}
```

Value conversions don't require a parameterless constructor on your SmartEnum.

If you want to store the SmartEnum itself in its own table (for example, a lookup table with both `Name` and `Value` columns), you still don't need a parameterless constructor. EF Core 2.1 and later can [bind to a constructor](https://learn.microsoft.com/en-us/ef/core/modeling/constructors) whose parameters match the mapped properties:

```csharp
public sealed class PolicyStatus : SmartEnum<PolicyStatus>
{
    public static readonly PolicyStatus Active = new(nameof(Active), 1);
    public static readonly PolicyStatus Inactive = new(nameof(Inactive), 2);

    private PolicyStatus(string name, int value) : base(name, value) { }
}

builder.Entity<PolicyStatus>(e =>
{
    e.HasKey(s => s.Value);
    e.Property(s => s.Value).ValueGeneratedNever();
    e.Property(s => s.Name);
});
```

Instances that EF Core loads this way are new objects, not the static fields. They compare equal with `==` and `Equals` (equality is by `Value`), but not with `ReferenceEquals`. See [issue #103](https://github.com/ardalis/SmartEnum/issues/103) for more background.

#### Using SmartEnum.EFCore

EF Core 6 introduced pre-convention model configuration which allows value conversions to be configured for specific types within a model. If you have installed `Ardalis.SmartEnum.EFCore` it is sufficient to add the following line at the beginning of the `ConfigureConventions` method:

```csharp
using Ardalis.SmartEnum.EFCore; // was SmartEnum.EFCore before 9.0

protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
    configurationBuilder.ConfigureSmartEnum();

    ...
}
```

For previous versions of EF Core, the following line can be added at the end of the `OnModelCreating` method:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    ...

    modelBuilder.ConfigureSmartEnum();
}
```

## AutoFixture support

New instances of a `SmartEnum` should not be created. Instead, references to the existing ones should always be used. [AutoFixture](https://github.com/AutoFixture/AutoFixture) by default doesn't know how to do this. The `Ardalis.SmartEnum.AutoFixture` package includes a specimen builder for `SmartEnum`. Simply add the customization to the `IFixture` builder:

```csharp
var fixture = new Fixture()
    .Customize(new SmartEnumCustomization());

var smartEnum = fixture.Create<TestEnum>();
```

## Guard Clauses support

The `Ardalis.SmartEnum.GuardClauses` package adds a `SmartEnumOutOfRange` extension to [Ardalis.GuardClauses](https://github.com/ardalis/GuardClauses). It returns the matching `SmartEnum` instance for a value, or throws a `SmartEnumNotFoundException` if there isn't one:

```csharp
using Ardalis.GuardClauses;
using Ardalis.SmartEnum.GuardClauses;

public void UpdateStatus(int statusValue)
{
    TestEnum status = Guard.Against.SmartEnumOutOfRange<TestEnum>(statusValue);
    // use status here
}
```

For a `SmartEnum` whose value type isn't `int`, pass both type arguments, e.g. `Guard.Against.SmartEnumOutOfRange<TestStringEnum, string>(input)`. You can also pass a custom `message`, or an `exceptionCreator` to throw your own exception type:

```csharp
var status = Guard.Against.SmartEnumOutOfRange<TestEnum>(statusValue,
    exceptionCreator: () => new ArgumentException("Invalid status."));
```

See the [package README](src/SmartEnum.GuardClauses/README.md) for more details.

## Json support

When serializing a `SmartEnum` to JSON, only one of the properties (`Value` or `Name`) should be used. 

### Json<span></span>.Net
[Json.NET](https://www.newtonsoft.com/json) by default doesn't know how to do this. The `Ardalis.SmartEnum.JsonNet` package includes a couple of converters to achieve this. Simply use the attribute [JsonConverterAttribute](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonConverter.htm) to assign one of the converters to the `SmartEnum` to be de/serialized. The converters have the same names as the System.Text.Json ones shown below, in the `Ardalis.SmartEnum.JsonNet` namespace.

### System<span></span>.Text<span></span>.Json
[System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json?view=net-8.0) by default doesn't know how to do this. The `Ardalis.SmartEnum.SystemTextJson` package includes a couple of converters to achieve this. Simply use the attribute [JsonConverterAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverterattribute?view=net-8.0) to assign one of the converters to the `SmartEnum` to be de/serialized:

```csharp
public class TestClass
{
    [JsonConverter(typeof(SmartEnumNameConverter<TestEnum,int>))]
    public TestEnum Property { get; set; }
}
```

uses the `Name`:

```json
{
  "Property": "One"
}
```

While this:

```csharp
public class TestClass
{
    [JsonConverter(typeof(SmartEnumValueConverter<TestEnum,int>))]
    public TestEnum Property { get; set; }
}
```

uses the `Value`:

```json
{
  "Property": 1
}
```

Note: The SmartFlagEnum works identically to the SmartEnum when being Serialized and Deserialized.

## Model Binding support

The `Ardalis.SmartEnum.ModelBinding` package lets ASP.NET Core MVC bind `SmartEnum` action parameters and model properties from route values, query strings, and form fields. Register the binder provider when adding controllers:

```csharp
using Ardalis.SmartEnum.ModelBinding;

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new SmartEnumBinderProvider());
});
```

Incoming values are matched against the enum's `Name`, ignoring case:

```csharp
[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    // GET /reservations?status=paid binds ReservationStatus.Paid
    [HttpGet]
    public IActionResult GetByStatus([FromQuery] ReservationStatus status) => Ok(status.Name);
}
```

The binder doesn't apply to JSON request bodies, which are handled by the input formatter. Use one of the [Json converters](#json-support) for those.

## Dapper support

To enable Dapper support for `SmartEnum` values, add a `SmartEnumTypeHandler` to `SqlMapper` for the
given `SmartEnum` type. There are two inheritors of `SmartEnumTypeHandler`:
`SmartEnumByNameTypeHandler`, which maps the Name of a `SmartEnum` to a database column, and
`SmartEnumByValueTypeHandler`, which maps the Value of a `SmartEnum` to a database column.

```csharp
// Maps the name of TestEnum objects (e.g. "One", "Two", or "Three") to a database column.
SqlMapper.AddTypeHandler(typeof(TestEnum), new SmartEnumByNameTypeHandler<TestEnum>());
```

```csharp
// Maps the value of TestEnum objects (e.g. 1, 2, or 3) to a database column.
SqlMapper.AddTypeHandler(typeof(TestEnum), new SmartEnumByValueTypeHandler<TestEnum>());
```

### DapperSmartEnum

To avoid needing to explicitly register a `SmartEnum` type with Dapper, it can be done automatically
by inheriting from `DapperSmartEnumByName` or `DapperSmartEnumByValue` instead of from `SmartEnum`.

```csharp
public class TestEnumByName : DapperSmartEnumByName<TestEnumByName>
{
    public static readonly TestEnumByName One = new TestEnumByName(1);
    public static readonly TestEnumByName Two = new TestEnumByName(2);
    public static readonly TestEnumByName Three = new TestEnumByName(3);

    protected TestEnumByName(int value, [CallerMemberName] string name = null) : base(name, value)
    {
    }
}
```

```csharp
public class TestEnumByValue : DapperSmartEnumByValue<TestEnumByValue>
{
    public static readonly TestEnumByValue One = new TestEnumByValue(1);
    public static readonly TestEnumByValue Two = new TestEnumByValue(2);
    public static readonly TestEnumByValue Three = new TestEnumByValue(3);

    protected TestEnumByValue(int value, [CallerMemberName] string name = null) : base(name, value)
    {
    }
}
```

Inheritors of `DapperSmartEnum` can be decorated with custom attributes in order to configure
its type handler. Use `DbTypeAttribute` (e.g. `[DbType(DbType.String)]`) to specify that parameters
should have their `DbType` property set to the specified value. Use `DoNotSetDbTypeAttribute` (e.g.
`[DoNotSetDbType]`) to specify that parameters should not have their `DbType` property set. Use
`IgnoreCaseAttribute` (e.g. `[IgnoreCase]`) when inheriting from `DapperSmartEnumByName` to specify
that database values do not need to match the case of a SmartEnum Name.

### Case Insensitive String Enum

When creating enums of strings, the default behaviour of SmartEnum is to compare the strings with a case sensitive comparer.
It is possible to specify a different equality comparer for the enum values, for example a case insensitive one:

```csharp
[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public class CaseInsensitiveEnum : SmartEnum<CaseInsensitiveEnum, string>
{
    protected CaseInsensitiveEnum(string name, string value) : base(name, value) { }

    public static readonly CaseInsensitiveEnum One = new CaseInsensitiveEnum("One", "one");
    public static readonly CaseInsensitiveEnum Two = new CaseInsensitiveEnum("Two", "two");
}

var e1 = CaseInsensitiveEnum.FromValue("ONE");
var e2 = CaseInsensitiveEnum.FromValue("one");

// e1 is equal to e2
```

## Name Validation Attribute

The DataAnnotations ValidationAttribute `SmartEnumNameAttribute` allows you to validate your models, mandating that when provided a value it must be matching the name of a given `SmartEnum`. This attribute allows `null` values (use `[Required]` to disallow nulls).

In addition to specifying the `SmartEnum` to match, you may also pass additional parameters:
- `allowCaseInsensitiveMatch` (default `false`)
- `errorMessage` (default `"{0} must be one of: {1}"`): A format string to customize the error
  - `{0}` is the name of the property being validated
  - `{1}` is the comma-separated list of valid `SmartEnum` names

### Example of Name Validation Attribute
```csharp
public sealed class ExampleSmartEnum : SmartEnum<ExampleSmartEnum>
{
    public static readonly ExampleSmartEnum Foo = new ExampleSmartEnum(nameof(Foo), 1);
    public static readonly ExampleSmartEnum Bar = new ExampleSmartEnum(nameof(Bar), 2);
    
    private ExampleSmartEnum(string name, int value) : base(name, value) { }
}

public class ExampleModel
{
    [Required]
    [SmartEnumName(typeof(ExampleSmartEnum))]
    public string MyExample { get; set; } // must be "Foo" or "Bar"
    
    [SmartEnumName(typeof(ExampleSmartEnum), allowCaseInsensitiveMatch: true)]
    public string CaseInsensitiveExample { get; set; } // "Foo", "foo", etc. allowed; null also allowed here
}
```

## Examples in the Real World

- [Race](https://github.com/peaklims/PeakLimsApi/blob/main/PeakLims/src/PeakLims/Domain/Races/Race.cs)

[Search for more](https://github.com/search?l=C%23&q=Ardalis.SmartEnum&type=Code)

## References

- [Listing Strongly Typed Enum Options in C#](https://ardalis.com/listing-strongly-typed-enum-options-in-c)
- [Enum Alternatives in C#](https://ardalis.com/enum-alternatives-in-c)
- [Smarter Enumerations (podcast episode)](https://www.weeklydevtips.com/014)
- [Persisting a Smart Enum with Entity Framework Core](https://blog.nimblepros.com/blogs/persisting-a-smart-enum-with-entity-framework-core/)
