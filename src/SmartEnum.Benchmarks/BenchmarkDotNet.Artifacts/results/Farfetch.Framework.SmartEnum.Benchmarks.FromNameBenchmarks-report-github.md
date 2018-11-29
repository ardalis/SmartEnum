``` ini

BenchmarkDotNet=v0.11.1, OS=macOS High Sierra 10.13.6 (17G3025) [Darwin 17.7.0]
Intel Core i5-7360U CPU 2.30GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|                                   Method |         Mean |       Error |        StdDev |       Median |   Library |  Gen 0 | Allocated |
|----------------------------------------- |-------------:|------------:|--------------:|-------------:|---------- |-------:|----------:|
|                        Enum_FromName_One |    220.30 ns |   1.2587 ns |     1.1774 ns |    220.07 ns |      Enum | 0.0114 |      24 B |
|                        Enum_FromName_Ten |    289.65 ns |   2.8237 ns |     2.6413 ns |    288.61 ns |      Enum | 0.0114 |      24 B |
|                    Enum_FromName_Invalid | 19,537.84 ns | 206.2382 ns |   172.2182 ns | 19,479.50 ns |      Enum | 0.3052 |     672 B |
|             Enum_FromName_one_IgnoreCase |    233.94 ns |   2.6366 ns |     2.4663 ns |    232.64 ns |      Enum | 0.0114 |      24 B |
|             Enum_FromName_ten_IgnoreCase |    329.88 ns |   1.6069 ns |     1.3418 ns |    329.63 ns |      Enum | 0.0114 |      24 B |
|         Enum_FromName_Invalid_IgnoreCase | 20,087.28 ns | 394.4027 ns |   690.7646 ns | 19,765.10 ns |      Enum | 0.3052 |     672 B |
|                    EnumsNET_FromName_One |     94.33 ns |   1.6721 ns |     1.5641 ns |     93.79 ns |  EnumsNET |      - |       0 B |
|                    EnumsNET_FromName_Ten |     96.82 ns |   2.5893 ns |     7.2607 ns |     94.34 ns |  EnumsNET |      - |       0 B |
|                EnumsNET_FromName_Invalid | 20,058.65 ns | 289.1719 ns |   270.4915 ns | 19,972.23 ns |  EnumsNET | 0.3052 |     696 B |
|                 EnumsNET_TryFromName_One |     90.97 ns |   1.7343 ns |     1.7033 ns |     90.13 ns |  EnumsNET |      - |       0 B |
|                 EnumsNET_TryFromName_Ten |     89.77 ns |   0.3545 ns |     0.3142 ns |     89.73 ns |  EnumsNET |      - |       0 B |
|             EnumsNET_TryFromName_Invalid |    132.78 ns |   0.6905 ns |     0.5391 ns |    132.68 ns |  EnumsNET |      - |       0 B |
|                      Constant_GetFor_One |     35.05 ns |   0.7913 ns |     1.5984 ns |     34.73 ns |  Constant |      - |       0 B |
|                      Constant_GetFor_Ten |     32.90 ns |   0.3696 ns |     0.3457 ns |     32.75 ns |  Constant |      - |       0 B |
|                  Constant_GetFor_Invalid | 17,958.66 ns | 357.8949 ns |   672.2134 ns | 17,771.88 ns |  Constant | 0.3052 |     688 B |
|             Constant_GetOrDefaultFor_One |     31.84 ns |   0.4769 ns |     0.4461 ns |     31.95 ns |  Constant |      - |       0 B |
|             Constant_GetOrDefaultFor_Ten |     33.26 ns |   0.7509 ns |     1.0769 ns |     33.18 ns |  Constant |      - |       0 B |
|         Constant_GetOrDefaultFor_Invalid |  2,799.20 ns |  55.8099 ns |   154.6489 ns |  2,764.20 ns |  Constant | 0.2441 |     520 B |
|          Ardalis_FromName_one_IgnoreCase |    212.44 ns |   4.2839 ns |     8.6536 ns |    209.01 ns |   Ardalis | 0.0608 |     128 B |
|          Ardalis_FromName_ten_IgnoreCase |    301.21 ns |   6.3462 ns |    14.5815 ns |    297.03 ns |   Ardalis | 0.0606 |     128 B |
|      Ardalis_FromName_Invalid_IgnoreCase | 18,194.75 ns | 353.3716 ns |   506.7950 ns | 18,116.01 ns |   Ardalis | 0.3052 |     640 B |
|                   SmartEnum_FromName_One |     24.03 ns |   0.5701 ns |     1.1385 ns |     23.70 ns | SmartEnum |      - |       0 B |
|                   SmartEnum_FromName_Ten |     23.62 ns |   0.6063 ns |     1.0296 ns |     23.15 ns | SmartEnum |      - |       0 B |
|               SmartEnum_FromName_Invalid | 20,423.48 ns | 905.6022 ns | 2,583.7341 ns | 19,400.12 ns | SmartEnum | 0.3357 |     728 B |
|        SmartEnum_FromName_one_IgnoreCase |     86.97 ns |   1.8499 ns |     3.1910 ns |     86.75 ns | SmartEnum |      - |       0 B |
|        SmartEnum_FromName_ten_IgnoreCase |     84.54 ns |   1.0116 ns |     0.8967 ns |     84.26 ns | SmartEnum |      - |       0 B |
|    SmartEnum_FromName_Invalid_IgnoreCase | 18,981.14 ns | 369.3238 ns |   410.5024 ns | 18,797.59 ns | SmartEnum | 0.3357 |     728 B |
|                SmartEnum_TryFromName_One |     21.66 ns |   0.3837 ns |     0.3589 ns |     21.54 ns | SmartEnum |      - |       0 B |
|                SmartEnum_TryFromName_Ten |     21.68 ns |   0.3953 ns |     0.3505 ns |     21.58 ns | SmartEnum |      - |       0 B |
|            SmartEnum_TryFromName_Invalid |     18.06 ns |   0.4233 ns |     0.3960 ns |     17.90 ns | SmartEnum |      - |       0 B |
|     SmartEnum_TryFromName_one_IgnoreCase |     87.31 ns |   1.4166 ns |     1.1829 ns |     87.56 ns | SmartEnum |      - |       0 B |
|     SmartEnum_TryFromName_ten_IgnoreCase |     87.79 ns |   1.7936 ns |     1.8419 ns |     87.19 ns | SmartEnum |      - |       0 B |
| SmartEnum_TryFromName_Invalid_IgnoreCase |     68.48 ns |   1.0099 ns |     0.8953 ns |     68.16 ns | SmartEnum |      - |       0 B |
