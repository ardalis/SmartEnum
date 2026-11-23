# Ardalis.SmartEnum.ProtoBufNet

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

[protobuf-net](https://github.com/protobuf-net/protobuf-net) (de)serialization support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). A SmartEnum should be serialized as either its `Name` or its `Value`, and this package provides surrogates for both.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.ProtoBufNet
```

## Usage

Register a surrogate for each SmartEnum type on your `RuntimeTypeModel`:

```csharp
using Ardalis.SmartEnum.ProtoBufNet;
using ProtoBuf.Meta;

var model = RuntimeTypeModel.Default;

// Serialize by Name
model.Add(typeof(TestEnum), false)
    .SetSurrogate(typeof(SmartEnumNameSurrogate<TestEnum, int>));

// ...or by Value
// model.Add(typeof(TestEnum), false)
//     .SetSurrogate(typeof(SmartEnumValueSurrogate<TestEnum, int>));
```

SmartEnum properties on your `[ProtoContract]` types are then serialized using the registered surrogate. For `SmartFlagEnum` types, use `SmartFlagEnumNameSurrogate<TEnum, TValue>` or `SmartFlagEnumValueSurrogate<TEnum, TValue>`.

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum) for full documentation.
