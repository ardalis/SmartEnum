namespace Ardalis.SmartEnum.SourceGenerator;

internal static class Constants
{

    public static string SmartEnumGeneratorAttribute = """
    namespace Ardalis.SmartEnum
    {
        [System.AttributeUsage(AttributeTargets.Class)]
        public class SmartEnumGeneratorAttribute : System.Attribute
        {
            
        }
    }
    """;
}

