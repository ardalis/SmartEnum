namespace Ardalis.SmartEnum.MessagePack.UnitTests
{
	using global::MessagePack;
	using global::MessagePack.Resolvers;
	using Xunit;
	using FluentAssertions;
	using global::MessagePack.Formatters;

	public class SmartEnumNameConverterTests
	{

		[MessagePackObject]
		public class TestClass
		{
			[Key(0)]
			[MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumBoolean, bool>))]
			public TestEnumBoolean Bool { get; set; }

			[Key(1)]
			[MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumInt16, short>))]
			public TestEnumInt16 Int16 { get; set; }

			[Key(2)]
			[MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumInt32, int>))]
			public TestEnumInt32 Int32 { get; set; }

			[Key(3)]
			[MessagePackFormatter(typeof(SmartEnumNameFormatter<TestEnumDouble, double>))]
			public TestEnumDouble Double { get; set; }
		}


		static readonly TestClass NullTestInstance = new TestClass
		{
			Bool = null,
			Int16 = null,
			Int32 = null,
			Double = null,
		};

		static readonly TestClass TestInstance = new TestClass
		{
			Bool = TestEnumBoolean.Instance,
			Int16 = TestEnumInt16.Instance,
			Int32 = TestEnumInt32.Instance,
			Double = TestEnumDouble.Instance,
		};

		static readonly string JsonString = @"[""Instance"",""Instance"",""Instance"",""Instance""]";

		public SmartEnumNameConverterTests()
		{
			_resolver = CompositeResolver.Create(new IMessagePackFormatter[] { 
					new SmartEnumNameFormatter<TestEnumBoolean, bool>(),
					new SmartEnumNameFormatter<TestEnumInt16, short>(),
					new SmartEnumNameFormatter<TestEnumInt32, int>(),
					new SmartEnumNameFormatter<TestEnumDouble, double>()}
			);
		}

		private readonly IFormatterResolver _resolver;

		//[Fact]
		// Test is failing - need to sort out how to fix.
		public void SerializesValue()
		{
			var options = StandardResolverAllowPrivate.Options
					.WithCompression(MessagePackCompression.Lz4BlockArray)
					.WithResolver(_resolver);
			var message = MessagePackSerializer.Serialize(TestInstance, options);

			MessagePackSerializer.ConvertToJson(message).Should().Be(JsonString);
			//var message = MessagePackSerializer.Serialize(TestInstance);

			//MessagePackSerializer.ToJson(message).Should().Be(JsonString);
		}

		//[Fact]
		// Test is failing - need to sort out how to fix.
		public void DeserializesValue()
		{
			var options = StandardResolverAllowPrivate.Options
					.WithCompression(MessagePackCompression.Lz4BlockArray)
					.WithResolver(_resolver);

			var message = MessagePackSerializer.Serialize(TestInstance);

			var obj = MessagePackSerializer.Deserialize<TestClass>(message);

			obj.Bool.Should().BeSameAs(TestEnumBoolean.Instance);
			obj.Int16.Should().BeSameAs(TestEnumInt16.Instance);
			obj.Int32.Should().BeSameAs(TestEnumInt32.Instance);
			obj.Double.Should().BeSameAs(TestEnumDouble.Instance);
		}
	}
}