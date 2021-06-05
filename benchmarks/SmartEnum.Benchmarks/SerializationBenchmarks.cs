namespace Ardalis.SmartEnum.Benchmarks
{
    using System.IO;
    using BenchmarkDotNet.Attributes;

    [Config(typeof(Config))]
    public class SerializationBenchmarks
    {
        [global::MessagePack.MessagePackObject]
        [global::ProtoBuf.ProtoContract]
        public sealed class TestEnumNameClass
        {
            [global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
            [global::Utf8Json.JsonFormatter(typeof(global::Utf8Json.Formatters.EnumFormatter<TestEnum>), true)]
            [global::MessagePack.Key(0)]
            [global::MessagePack.MessagePackFormatter(typeof(global::MessagePack.Formatters.EnumAsStringFormatter<TestEnum>))]
            [global::ProtoBuf.ProtoMember(1)]
            public TestEnum Enum { get; set; }
        }

        [global::MessagePack.MessagePackObject]
        [global::ProtoBuf.ProtoContract]
        public sealed class TestEnumValueClass
        {
            [global::Utf8Json.JsonFormatter(typeof(global::Utf8Json.Formatters.EnumFormatter<TestEnum>), false)]
            [global::MessagePack.Key(0)]
            [global::ProtoBuf.ProtoMember(1)]
            public TestEnum Enum { get; set; }
        }

        [global::MessagePack.MessagePackObject]
        [global::ProtoBuf.ProtoContract]
        public sealed class TestSmartEnumNameClass
        {
            [global::Newtonsoft.Json.JsonConverter(typeof(JsonNet.SmartEnumNameConverter<TestSmartEnum, int>))]
            [global::Utf8Json.JsonFormatter(typeof(Utf8Json.SmartEnumNameFormatter<TestSmartEnum, int>))]
            [global::MessagePack.Key(0)]
            [global::MessagePack.MessagePackFormatter(typeof(MessagePack.SmartEnumNameFormatter<TestSmartEnum, int>))]
            [global::ProtoBuf.ProtoMember(1)]
            public TestSmartEnum Enum { get; set; }
        }

        [global::MessagePack.MessagePackObject]
        [global::ProtoBuf.ProtoContract]
        public sealed class TestSmartEnumValueClass
        {
            [global::Newtonsoft.Json.JsonConverter(typeof(JsonNet.SmartEnumValueConverter<TestSmartEnum, int>))]
            [global::Utf8Json.JsonFormatter(typeof(Utf8Json.SmartEnumValueFormatter<TestSmartEnum, int>))]
            [global::MessagePack.Key(0)]
            [global::MessagePack.MessagePackFormatter(typeof(MessagePack.SmartEnumValueFormatter<TestSmartEnum, int>))]
            [global::ProtoBuf.ProtoMember(1)]
            public TestSmartEnum Enum { get; set; }
        }

        static readonly TestEnumNameClass nameEnumInstance = new TestEnumNameClass { Enum = TestEnum.One };
        static readonly TestEnumValueClass valueEnumInstance = new TestEnumValueClass { Enum = TestEnum.One };
        static readonly TestSmartEnumNameClass nameSmartEnumInstance = new TestSmartEnumNameClass { Enum = TestSmartEnum.One };
        static readonly TestSmartEnumValueClass valueSmartEnumInstance = new TestSmartEnumValueClass { Enum = TestSmartEnum.One };
        static readonly string nameJson = @"{""Enum"":""One""}";
        static readonly string valueJson = @"{""Enum"":1}";
        static byte[] nameMessagePack;
        static byte[] valueMessagePack;
        static global::ProtoBuf.Meta.RuntimeTypeModel nameModel;
        static global::ProtoBuf.Meta.RuntimeTypeModel valueModel;
        static Stream serializeStream;
        static Stream enumProtoBufDeserializeStream;
        static Stream nameProtoBufDeserializeStream;
        static Stream valueProtoBufDeserializeStream;

        [GlobalSetup]
        public void GlobalSetup()
        {
            global::Utf8Json.Resolvers.CompositeResolver.Register(
                new Utf8Json.SmartEnumNameFormatter<TestSmartEnum, int>(),
                new Utf8Json.SmartEnumValueFormatter<TestSmartEnum, int>());
            global::MessagePack.Resolvers.CompositeResolver.Register(
                new MessagePack.SmartEnumNameFormatter<TestSmartEnum, int>(),
                new MessagePack.SmartEnumValueFormatter<TestSmartEnum, int>());

            nameMessagePack = global::MessagePack.MessagePackSerializer.Serialize(nameEnumInstance);
            valueMessagePack = global::MessagePack.MessagePackSerializer.Serialize(valueEnumInstance);

            serializeStream = new MemoryStream();

            enumProtoBufDeserializeStream = new MemoryStream();
            global::ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(enumProtoBufDeserializeStream, valueEnumInstance);

            nameProtoBufDeserializeStream = new MemoryStream();
            nameModel = global::ProtoBuf.Meta.RuntimeTypeModel.Create();
            nameModel.Add(typeof(TestSmartEnum), false).SetSurrogate(typeof(ProtoBufNet.SmartEnumNameSurrogate<TestSmartEnum, int>));
            nameModel.Serialize(nameProtoBufDeserializeStream, nameSmartEnumInstance);

            valueProtoBufDeserializeStream = new MemoryStream();
            valueModel = global::ProtoBuf.Meta.RuntimeTypeModel.Create();
            valueModel.Add(typeof(TestSmartEnum), false).SetSurrogate(typeof(ProtoBufNet.SmartEnumValueSurrogate<TestSmartEnum, int>));
            valueModel.Serialize(valueProtoBufDeserializeStream, valueSmartEnumInstance);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            serializeStream.Dispose();
            enumProtoBufDeserializeStream.Dispose();
            nameProtoBufDeserializeStream.Dispose();
            valueProtoBufDeserializeStream.Dispose();
        }

        ////////////////////////////////////////////////////////////////////////////////
        // JsonNet

        [Benchmark]
        public string JsonNet_Enum_Serialize_Name() => global::Newtonsoft.Json.JsonConvert.SerializeObject(nameEnumInstance);

        [Benchmark]
        public string JsonNet_Enum_Serialize_Value() => global::Newtonsoft.Json.JsonConvert.SerializeObject(valueEnumInstance);

        [Benchmark]
        public TestEnumNameClass JsonNet_Enum_Deserialize_Name() => global::Newtonsoft.Json.JsonConvert.DeserializeObject<TestEnumNameClass>(nameJson);

        [Benchmark]
        public TestEnumValueClass JsonNet_Enum_Deserialize_Value() => global::Newtonsoft.Json.JsonConvert.DeserializeObject<TestEnumValueClass>(valueJson);

        [Benchmark]
        public string JsonNet_SmartEnum_Serialize_Name() => global::Newtonsoft.Json.JsonConvert.SerializeObject(nameSmartEnumInstance);

        [Benchmark]
        public string JsonNet_SmartEnum_Serialize_Value() => global::Newtonsoft.Json.JsonConvert.SerializeObject(valueSmartEnumInstance);

        [Benchmark]
        public TestSmartEnumNameClass JsonNet_SmartEnum_Deserialize_Name() => global::Newtonsoft.Json.JsonConvert.DeserializeObject<TestSmartEnumNameClass>(nameJson);

        [Benchmark]
        public TestSmartEnumValueClass JsonNet_SmartEnum_Deserialize_Value() => global::Newtonsoft.Json.JsonConvert.DeserializeObject<TestSmartEnumValueClass>(valueJson);

        ////////////////////////////////////////////////////////////////////////////////
        // Utf8Json

        [Benchmark]
        public byte[] Utf8Json_Enum_Serialize_Name() => global::Utf8Json.JsonSerializer.Serialize(nameEnumInstance);

        [Benchmark]
        public byte[] Utf8Json_Enum_Serialize_Value() => global::Utf8Json.JsonSerializer.Serialize(valueEnumInstance);

        [Benchmark]
        public TestEnumNameClass Utf8Json_Enum_Deserialize_Name() => global::Utf8Json.JsonSerializer.Deserialize<TestEnumNameClass>(nameJson);

        [Benchmark]
        public TestEnumValueClass Utf8Json_Enum_Deserialize_Value() => global::Utf8Json.JsonSerializer.Deserialize<TestEnumValueClass>(valueJson);

        [Benchmark]
        public byte[] Utf8Json_SmartEnum_Serialize_Name() => global::Utf8Json.JsonSerializer.Serialize(nameSmartEnumInstance);

        [Benchmark]
        public byte[] Utf8Json_SmartEnum_Serialize_Value() => global::Utf8Json.JsonSerializer.Serialize(valueSmartEnumInstance);

        [Benchmark]
        public TestSmartEnumNameClass Utf8Json_SmartEnum_Deserialize_Name() => global::Utf8Json.JsonSerializer.Deserialize<TestSmartEnumNameClass>(nameJson);

        [Benchmark]
        public TestSmartEnumValueClass Utf8Json_SmartEnum_Deserialize_Value() => global::Utf8Json.JsonSerializer.Deserialize<TestSmartEnumValueClass>(valueJson);

        ////////////////////////////////////////////////////////////////////////////////
        // MessagePack

        [Benchmark]
        public byte[] MessagePack_Enum_Serialize_Name() => global::MessagePack.MessagePackSerializer.Serialize(nameEnumInstance);

        [Benchmark]
        public byte[] MessagePack_Enum_Serialize_Value() => global::MessagePack.MessagePackSerializer.Serialize(valueEnumInstance);

        [Benchmark]
        public TestEnumNameClass MessagePack_Enum_Deserialize_Name() => global::MessagePack.MessagePackSerializer.Deserialize<TestEnumNameClass>(nameMessagePack);

        [Benchmark]
        public TestEnumValueClass MessagePack_Enum_Deserialize_Value() => global::MessagePack.MessagePackSerializer.Deserialize<TestEnumValueClass>(valueMessagePack);

        [Benchmark]
        public byte[] MessagePack_SmartEnum_Serialize_Name() => global::MessagePack.MessagePackSerializer.Serialize(nameSmartEnumInstance);

        [Benchmark]
        public byte[] MessagePack_SmartEnum_Serialize_Value() => global::MessagePack.MessagePackSerializer.Serialize(valueSmartEnumInstance);

        [Benchmark]
        public TestSmartEnumNameClass MessagePack_SmartEnum_Deserialize_Name() => global::MessagePack.MessagePackSerializer.Deserialize<TestSmartEnumNameClass>(nameMessagePack);

        [Benchmark]
        public TestSmartEnumValueClass MessagePack_SmartEnum_Deserialize_Value() => global::MessagePack.MessagePackSerializer.Deserialize<TestSmartEnumValueClass>(valueMessagePack);

        ////////////////////////////////////////////////////////////////////////////////
        // ProtoBufNet

        [Benchmark]
        public void ProtoBufNet_Enum_Serialize_Value() => global::ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(serializeStream, valueEnumInstance);

        [Benchmark]
        public TestEnumValueClass ProtoBufNet_Enum_Deserialize_Value()
        {
            valueProtoBufDeserializeStream.Seek(0, SeekOrigin.Begin);
            return (TestEnumValueClass)global::ProtoBuf.Meta.RuntimeTypeModel.Default.Deserialize(enumProtoBufDeserializeStream, null, typeof(TestEnumValueClass));
        }

        [Benchmark]
        public void ProtoBufNet_SmartEnum_Serialize_Name() => nameModel.Serialize(serializeStream, nameSmartEnumInstance);

        [Benchmark]
        public void ProtoBufNet_SmartEnum_Serialize_Value() => valueModel.Serialize(serializeStream, valueSmartEnumInstance);

        [Benchmark]
        public TestSmartEnumNameClass ProtoBufNet_SmartEnum_Deserialize_Name()
        {
            nameProtoBufDeserializeStream.Seek(0, SeekOrigin.Begin);
            return (TestSmartEnumNameClass)nameModel.Deserialize(nameProtoBufDeserializeStream, null, typeof(TestSmartEnumNameClass));
        }

        [Benchmark]
        public TestSmartEnumValueClass ProtoBufNet_SmartEnum_Deserialize_Value()
        {
            valueProtoBufDeserializeStream.Seek(0, SeekOrigin.Begin);
            return (TestSmartEnumValueClass)valueModel.Deserialize(valueProtoBufDeserializeStream, null, typeof(TestSmartEnumValueClass));
        }
    }
}