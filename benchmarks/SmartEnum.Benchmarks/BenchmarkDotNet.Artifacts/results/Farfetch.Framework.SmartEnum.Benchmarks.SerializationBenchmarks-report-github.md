``` ini

BenchmarkDotNet=v0.11.1, OS=macOS High Sierra 10.13.6 (17G3025) [Darwin 17.7.0]
Intel Core i5-7360U CPU 2.30GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|                                  Method |        Mean |      Error |     StdDev |     Library |  Gen 0 | Allocated |
|---------------------------------------- |------------:|-----------:|-----------:|------------ |-------:|----------:|
|             JsonNet_Enum_Serialize_Name |   992.05 ns | 19.4948 ns | 33.1037 ns |     JsonNet | 0.7734 |    1624 B |
|            JsonNet_Enum_Serialize_Value |   566.43 ns | 11.1941 ns | 14.5554 ns |     JsonNet | 0.6056 |    1272 B |
|           JsonNet_Enum_Deserialize_Name | 1,407.09 ns | 27.4698 ns | 25.6953 ns |     JsonNet | 1.3981 |    2936 B |
|          JsonNet_Enum_Deserialize_Value | 1,311.58 ns | 20.8171 ns | 17.3832 ns |     JsonNet | 1.2760 |    2680 B |
|        JsonNet_SmartEnum_Serialize_Name |   712.34 ns | 16.3263 ns | 15.2716 ns |     JsonNet | 0.7620 |    1600 B |
|       JsonNet_SmartEnum_Serialize_Value |   768.42 ns |  5.7626 ns |  5.1084 ns |     JsonNet | 0.7238 |    1520 B |
|      JsonNet_SmartEnum_Deserialize_Name |   913.36 ns |  4.8423 ns |  4.2926 ns |     JsonNet | 1.3762 |    2888 B |
|     JsonNet_SmartEnum_Deserialize_Value |   943.95 ns |  6.1076 ns |  5.4142 ns |     JsonNet | 1.3838 |    2904 B |
|            Utf8Json_Enum_Serialize_Name |    95.47 ns |  0.4791 ns |  0.4481 ns |    Utf8Json | 0.0190 |      40 B |
|           Utf8Json_Enum_Serialize_Value |    70.55 ns |  0.6986 ns |  0.6193 ns |    Utf8Json | 0.0190 |      40 B |
|          Utf8Json_Enum_Deserialize_Name |   232.96 ns |  2.0785 ns |  1.9442 ns |    Utf8Json | 0.0303 |      64 B |
|         Utf8Json_Enum_Deserialize_Value |   176.59 ns |  0.7716 ns |  0.7218 ns |    Utf8Json | 0.0303 |      64 B |
|       Utf8Json_SmartEnum_Serialize_Name |    81.66 ns |  0.4903 ns |  0.4094 ns |    Utf8Json | 0.0190 |      40 B |
|      Utf8Json_SmartEnum_Serialize_Value |    66.85 ns |  0.4746 ns |  0.4439 ns |    Utf8Json | 0.0190 |      40 B |
|     Utf8Json_SmartEnum_Deserialize_Name |   282.00 ns |  2.5220 ns |  2.3591 ns |    Utf8Json | 0.0453 |      96 B |
|    Utf8Json_SmartEnum_Deserialize_Value |   195.31 ns |  1.4756 ns |  1.3803 ns |    Utf8Json | 0.0303 |      64 B |
|         MessagePack_Enum_Serialize_Name |    87.39 ns |  0.6092 ns |  0.5401 ns | MessagePack | 0.0151 |      32 B |
|        MessagePack_Enum_Serialize_Value |    57.37 ns |  0.5462 ns |  0.5109 ns | MessagePack | 0.0151 |      32 B |
|       MessagePack_Enum_Deserialize_Name |    95.40 ns |  0.6211 ns |  0.5810 ns | MessagePack | 0.0266 |      56 B |
|      MessagePack_Enum_Deserialize_Value |    39.08 ns |  0.5608 ns |  0.4972 ns | MessagePack | 0.0114 |      24 B |
|    MessagePack_SmartEnum_Serialize_Name |    78.12 ns |  0.6011 ns |  0.5623 ns | MessagePack | 0.0151 |      32 B |
|   MessagePack_SmartEnum_Serialize_Value |    51.51 ns |  0.4989 ns |  0.4666 ns | MessagePack | 0.0152 |      32 B |
|  MessagePack_SmartEnum_Deserialize_Name |    98.59 ns |  0.6671 ns |  0.6240 ns | MessagePack | 0.0266 |      56 B |
| MessagePack_SmartEnum_Deserialize_Value |    58.34 ns |  0.3679 ns |  0.2873 ns | MessagePack | 0.0113 |      24 B |
|        ProtoBufNet_Enum_Serialize_Value |   498.42 ns |  4.7729 ns |  4.2311 ns | ProtoBufNet | 0.0992 |     208 B |
|      ProtoBufNet_Enum_Deserialize_Value |   633.80 ns |  4.8538 ns |  4.5403 ns | ProtoBufNet | 0.0381 |      80 B |
|    ProtoBufNet_SmartEnum_Serialize_Name |   566.92 ns |  7.7919 ns |  6.5066 ns | ProtoBufNet | 0.1106 |     232 B |
|   ProtoBufNet_SmartEnum_Serialize_Value |   517.62 ns |  8.1628 ns |  7.2361 ns | ProtoBufNet | 0.1106 |     232 B |
|  ProtoBufNet_SmartEnum_Deserialize_Name | 1,348.35 ns | 10.0399 ns |  8.9001 ns | ProtoBufNet | 0.1659 |     352 B |
| ProtoBufNet_SmartEnum_Deserialize_Value |   966.51 ns |  9.6947 ns |  9.0685 ns | ProtoBufNet | 0.0496 |     104 B |
