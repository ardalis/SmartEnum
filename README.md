[![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum) 
![Last Publish Ardalis.SmartEnum to NuGet](https://github.com/ardalis/SmartEnum/workflows/publish%20Ardalis.SmartEnum%20to%20nuget/badge.svg)

### Sub-packages

SmartEnum.AutoFixture: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.AutoFixture.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.AutoFixture)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.AutoFixture.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.AutoFixture)![publish SmartEnum.AutoFixture to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.AutoFixture%20to%20nuget/badge.svg)

SmartEnum.JsonNet: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.JsonNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.JsonNet)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.JsonNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.JsonNet)![publish jsonnet to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.JsonNet%20to%20nuget/badge.svg)

SmartEnum.MessagePack: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.MessagePack.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.MessagePack)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.MessagePack.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.MessagePack)![publish SmartEnum.MessagePack to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.MessagePack%20to%20nuget/badge.svg)

SmartEnum.ProtoBufNet: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.ProtoBufNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.ProtoBufNet)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.ProtoBufNet.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.ProtoBufNet)![publish SmartEnum.ProtoBufNet to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.ProtoBufNet%20to%20nuget/badge.svg)

SmartEnum.SystemTextJson: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.SystemTextJson.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.SystemTextJson)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.SystemTextJson.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.SystemTextJson)![publish SmartEnum.SystemTextJson to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.SystemTextJson%20to%20nuget/badge.svg)

SmartEnum.Utf8Json: [![NuGet](https://img.shields.io/nuget/v/Ardalis.SmartEnum.Utf8Json.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.Utf8Json)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.Utf8Json.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum.Utf8Json)![publish SmartEnum.Utf8Json to nuget](https://github.com/ardalis/SmartEnum/workflows/publish%20SmartEnum.Utf8Json%20to%20nuget/badge.svg)

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

# Smart Enum

An implementation of a [type-safe object-oriented alternative](https://codeblog.jonskeet.uk/2006/01/05/classenum/) to [C# enum](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/enum).

## Contributors

Thanks to [Scott Depouw](https://github.com/sdepouw) and [Antão Almada](https://github.com/aalmada) for help with this project!

# Install

The framework is provided as a set of NuGet packages.

To install the minimum requirements:

```
Install-Package Ardalis.SmartEnum
```

To install support for serialization, AutoFixture or EF Core select the lines that apply:

```
Install-Package Ardalis.SmartEnum.AutoFixture
Install-Package Ardalis.SmartEnum.JsonNet
Install-Package Ardalis.SmartEnum.Utf8Json
Install-Package Ardalis.SmartEnum.MessagePack
Install-Package Ardalis.SmartEnum.ProtoBufNet
Install-Package Ardalis.SmartEnum.EFCore
```

## Usage

Define your smart enum by inheriting from `SmartEnum<TEnum>` where `TEnum` is the type you're declaring. For [example](/src/SmartEnum.UnitTests/TestEnum.cs):

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
    // public static TestEnum Three = new TestEnum(nameof(Three), 4); -> throws exception

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

Given an instance of a TestEnum, switch depending on value:

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

N.B. For performance critical code the fluent interface carries some overhead that you may wish to avoid. See the available [benchmarks](src/SmartEnum.Benchmarks) code for your use case.

### SmartFlagEnum

Support has been added for a `Flag` functionality.  
This feature is similar to the behaviour seen when applying the `[Flag]` attribute to Enums in the .NET Framework
All methods available on the `SmartFlagEnum` class return an `IEnumerable<SmartFlagEnum>` with one or more values depending on the value provided/method called.
Some Functionality is shared with the original SmartEnum class, listed below are the variations.

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

This behaviour can be disabled by applying the `AllowUnsafeFlagEnumValuesAttribute` to the smart enum class.  Note: If power of two values are not provided the SmarFlagEnum will not behave as expected!  

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
        public static readonly SmartFlagTestEnum CardAndCash = new SmartFlagTestEnum(nameof(CardAndCash), 3); -- Explicit `Combination` value
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
    
    var myFlagEnums = FromValue(3) -- Works!
    -and-
    var myFlagEnums = FromValue(5) -- Works!
    -but-
    Var myFlagEnums = FromValue(4) -- will throw an exception :(
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
            
                => "Director earns $100000 bonus this year.\n"
                   "Manager earns $10000 bonus this year.\n"
        }
         
        public void UseSmartFlagEnumTwo()
        {
            EmployeeType.FromValueToString(-1)
                => "Director, Manager, Assistant"
        }
            
        public void UseSmartFlagEnumTwo()
        {
            EmployeeType.FromValueToString(EmployeeType.Assistant | EmployeeType.Director)
                => "Director, Assistant"
        }
    }
  
```

### FromName()

Access an `IEnumerable` of enum instances by matching a string containing one or more enum names seperated by commas to its `Names` property:

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

Note: Negative values other than (-1) passed into this method will cause a `NegativeValueArgumentException` to be thrown, this behaviour can be disabled by applying the `AllowNegativeInput` attribute to the desired `SmartFlagEnum` class.

```csharp
[AllowNegativeInput]
public class SmartFlagTestEnum : SmartFlagEnum<SmartFlagTestEnum>
    {
        public static readonly SmartFlagTestEnum None = new SmartFlagTestEnum(nameof(None), 0);
        public static readonly SmartFlagTestEnum Card = new SmartFlagTestEnum(nameof(Card), 1);

        public SmartFlagTestEnum(string name, int value) : base(name, value)
        {
        }
    }
```

Note: `FromValue()` will accept any input that can be succesfully parsed as an integer.  If an invalid value is supplied it will throw an `InvalidFlagEnumValueParseException`.  

### FromValueToString()

Return a string representation of a series of enum instances name's:

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

Note: Negative values other than (-1) passed into this method will cause a `NegativeValueArgumentException` to be thrown, this behaviour can be disabled by applying the `AllowNegativeInput` attribute to the desired `SmartFlagEnum` class.

### BitWiseOrOperator

The `FromValue()` methods allow the Or ( | ) operator to be used to `add` enum values together and provide multiple values at once.

```csharp
var myFlagEnums = TestFlagEnum.FromValue(TestFlagEnum.One | TestFlagEnum.Two);
```

This will only work where the type of the `SmartFlagEnum` has been specified as `Int32` or else can be explicitly cast as an `Int32`.

```csharp
var myFlagEnums = TestFlagEnumDecimal.FromValue((int)TestFlagEnum.One | (int)TestFlagEnum.Two);
```

### Persisting with EF Core 2.1 or higher

EF Core 2.1 introduced [value conversions](https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions) which can be used to map SmartEnum types to simple database types. For example, given an entity named `Policy` with a property `PolicyStatus` that is a SmartEnum, you could use the following code to persist just the value to the database:

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

#### Using SmartEnum.EFCore

If you have installed `Ardalis.SmartEnum.EFCore` it is sufficient to add the following line at the end of the `OnModelCreating` method:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    ...
    
    modelBuilder.ConfigureSmartEnum();
}
```

## AutoFixture support

New instance of a `SmartEnum` should not be created. Instead, references to the existing ones should always be used. [AutoFixture](https://github.com/AutoFixture/AutoFixture) by default doesn't know how to do this. The `Ardalis.SmartEnum.AutoFixture` package includes a specimen builder for `SmartEnum`. Simply add the customization to the `IFixture` builder:

```csharp
var fixture = new Fixture()
    .Customize(new SmartEnumCustomization());

var smartEnum = fixture.Create<TestEnum>();
```

## Json<span></span>.NET support

When serializing a `SmartEnum` to JSON, only one of the properties (`Value` or `Name`) should be used. [Json.NET](https://www.newtonsoft.com/json) by default doesn't know how to do this. The `Ardalis.SmartEnum.JsonNet` package includes a couple of converters to achieve this. Simply use the attribute [JsonConverterAttribute](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonConverter.htm) to assign one of the converters to the `SmartEnum` to be de/serialized:

```csharp
public class TestClass
{
    [JsonConverter(typeof(SmartEnumNameConverter<int>))]
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
    [JsonConverter(typeof(SmartEnumValueConverter<int>))]
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
## References

- [Listing Strongly Typed Enums...)](https://ardalis.com/listing-strongly-typed-enum-options-in-c)
- [Enum Alternatives in C#](https://ardalis.com/enum-alternatives-in-c)
- [Smarter Enumerations (podcast episode)](http://www.weeklydevtips.com/014)

