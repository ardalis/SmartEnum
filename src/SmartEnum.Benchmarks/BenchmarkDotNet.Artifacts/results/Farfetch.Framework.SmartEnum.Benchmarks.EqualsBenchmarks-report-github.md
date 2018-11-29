``` ini

BenchmarkDotNet=v0.11.1, OS=macOS High Sierra 10.13.6 (17G3025) [Darwin 17.7.0]
Intel Core i5-7360U CPU 2.30GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|                          Method |       Mean |     Error |    StdDev |     Median |   Library |  Gen 0 | Allocated |
|-------------------------------- |-----------:|----------:|----------:|-----------:|---------- |-------:|----------:|
|                Enum_GetHashCode |  0.0016 ns | 0.0063 ns | 0.0056 ns |  0.0000 ns |      Enum |      - |       0 B |
|           Enum_Equals_SameValue | 14.0599 ns | 0.3943 ns | 0.4987 ns | 13.9135 ns |      Enum | 0.0229 |      48 B |
|      Enum_Equals_DifferentValue | 13.8287 ns | 0.1401 ns | 0.1242 ns | 13.7874 ns |      Enum | 0.0229 |      48 B |
|       Enum_Equals_DifferentType | 11.3394 ns | 0.1624 ns | 0.1519 ns | 11.2961 ns |      Enum | 0.0229 |      48 B |
|              Enum_EqualOperator |  0.0006 ns | 0.0020 ns | 0.0018 ns |  0.0000 ns |      Enum |      - |       0 B |
|           Enum_NotEqualOperator |  0.0029 ns | 0.0059 ns | 0.0055 ns |  0.0000 ns |      Enum |      - |       0 B |
|            Constant_GetHashCode |  9.0170 ns | 0.2121 ns | 0.1984 ns |  8.9488 ns |  Constant |      - |       0 B |
|            Constant_Equals_Null |  2.3111 ns | 0.0279 ns | 0.0247 ns |  2.3066 ns |  Constant |      - |       0 B |
|       Constant_Equals_SameValue |  2.3230 ns | 0.0297 ns | 0.0263 ns |  2.3290 ns |  Constant |      - |       0 B |
|  Constant_Equals_DifferentValue | 15.3502 ns | 0.3369 ns | 0.3152 ns | 15.2437 ns |  Constant |      - |       0 B |
|   Constant_Equals_DifferentType |  7.1682 ns | 0.0751 ns | 0.0627 ns |  7.1483 ns |  Constant |      - |       0 B |
|          Constant_EqualOperator |  9.2394 ns | 0.0852 ns | 0.0797 ns |  9.2414 ns |  Constant |      - |       0 B |
|       Constant_NotEqualOperator | 10.1589 ns | 0.1829 ns | 0.1711 ns | 10.0859 ns |  Constant |      - |       0 B |
|             Ardalis_GetHashCode | 26.8611 ns | 0.2908 ns | 0.2428 ns | 26.8645 ns |   Ardalis | 0.0152 |      32 B |
|             Ardalis_Equals_Null |  2.6078 ns | 0.0376 ns | 0.0333 ns |  2.5934 ns |   Ardalis |      - |       0 B |
|        Ardalis_Equals_SameValue |  2.1612 ns | 0.1185 ns | 0.1809 ns |  2.0697 ns |   Ardalis |      - |       0 B |
|   Ardalis_Equals_DifferentValue | 11.2824 ns | 0.2071 ns | 0.1938 ns | 11.2472 ns |   Ardalis |      - |       0 B |
|    Ardalis_Equals_DifferentType |  7.6414 ns | 0.1569 ns | 0.1468 ns |  7.6183 ns |   Ardalis |      - |       0 B |
|           Ardalis_EqualOperator | 10.0053 ns | 0.1062 ns | 0.0993 ns |  9.9746 ns |   Ardalis |      - |       0 B |
|        Ardalis_NotEqualOperator | 11.1820 ns | 0.0755 ns | 0.0669 ns | 11.1700 ns |   Ardalis |      - |       0 B |
|           SmartEnum_GetHashCode |  1.2080 ns | 0.0571 ns | 0.0635 ns |  1.2010 ns | SmartEnum |      - |       0 B |
|           SmartEnum_Equals_Null |  1.4195 ns | 0.0327 ns | 0.0306 ns |  1.4086 ns | SmartEnum |      - |       0 B |
|      SmartEnum_Equals_SameValue |  1.9837 ns | 0.0357 ns | 0.0334 ns |  1.9793 ns | SmartEnum |      - |       0 B |
| SmartEnum_Equals_DifferentValue |  2.6676 ns | 0.0849 ns | 0.0794 ns |  2.6458 ns | SmartEnum |      - |       0 B |
|  SmartEnum_Equals_DifferentType |  6.4906 ns | 0.0487 ns | 0.0432 ns |  6.4924 ns | SmartEnum |      - |       0 B |
|         SmartEnum_EqualOperator |  3.4570 ns | 0.0778 ns | 0.0727 ns |  3.4277 ns | SmartEnum |      - |       0 B |
|      SmartEnum_NotEqualOperator |  3.5695 ns | 0.1335 ns | 0.1311 ns |  3.5202 ns | SmartEnum |      - |       0 B |
