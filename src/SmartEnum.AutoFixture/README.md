# Ardalis.SmartEnum.AutoFixture

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

[AutoFixture](https://github.com/AutoFixture/AutoFixture) support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). New SmartEnum instances should never be created; code should always use the existing static instances. This package teaches AutoFixture to pick one of those instead of constructing a new one.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.AutoFixture
```

## Usage

Add `SmartEnumCustomization` to your fixture:

```csharp
using Ardalis.SmartEnum.AutoFixture;
using AutoFixture;

var fixture = new Fixture()
    .Customize(new SmartEnumCustomization());

var smartEnum = fixture.Create<TestEnum>(); // one of TestEnum's defined values
```

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum#autofixture-support) for full documentation.
