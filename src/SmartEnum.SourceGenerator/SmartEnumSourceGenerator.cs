using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using SmartEnum.SourceGenerator;

namespace Ardalis.SmartEnum.SourceGenerator;

[Generator]
public class SmartEnumSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        //attach debugger when debugging
        //if (!Debugger.IsAttached)
        //{
        //    Debugger.Launch();
        //}
#endif

        context.RegisterForSyntaxNotifications(() => new CustomSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // add SmartEnum generator attribute
        context.AddSource(
            "SmartEnumGeneratorAttribute.g.cs",
            SourceText.From(Constants.SmartEnumGeneratorAttribute, Encoding.UTF8));
        
        // get classes with attributes count > 0
        var syntaxReceiver = (CustomSyntaxReceiver)context.SyntaxReceiver;
        var cdss = syntaxReceiver?.Classes;
        foreach (var cds in cdss)
        {
            if (cds == null)
            {
                continue;
            }

            // check if class have SmartEnumGenerator attribute
            if (!cds.AttributeLists.Any(x => x.Attributes.Any(xx => xx.Name.ToFullString() == "SmartEnumGenerator")))
            {
                continue;
            }

            // get class namespace
            var cnamespace = (
                (QualifiedNameSyntax)cds.Parent.ChildNodes().FirstOrDefault(x => x.GetType() == typeof(QualifiedNameSyntax))
                ).ToFullString();


            // get class name
            var cname = cds.Identifier.ToString();

            // check if class is not partial then skipped it
            if (!cds.Modifiers.Any(x => x.ValueText == "partial"))
            {
                continue;
            }

            var ctype = GeneratedClassType.Pure;
            var ctypeStr = $" : SmartEnum <{cname}>";
            var smartEnumType = cds.BaseList?.Types.Any(x => x?.ToString() == $"SmartEnum<{cname}>") ?? false;
            if(smartEnumType)
            {
                ctype = GeneratedClassType.SmartEnum;
                ctypeStr = $"";
            }

            var smartFlagEnumType = cds.BaseList?.Types.Any(x => x?.ToString() == $"SmartFlagEnum<{cname}>") ?? false;
            if (smartFlagEnumType)
            {
                ctype = GeneratedClassType.SmartFlagEnum;
                ctypeStr = $"";
            }


            var variables = new List<VariableInfo>();

            // get class nodes
            foreach (var node in cds.ChildNodes())
            {
                // get fields
                if (node is FieldDeclarationSyntax { Declaration.Type: IdentifierNameSyntax ids } fds)
                {

                    var fieldType = ids.Identifier.Text;
                    if (fieldType != cname)
                    {
                        // only field with same type of class supported for now, custom class will supported in features
                        continue;
                    }

                    var fieldName = fds.Declaration.Variables.First().GetText().ToString();
                    variables.Add(new VariableInfo(fieldName, fieldType));
                }
            }

            // build generated class

            string ctemplateStart = $$"""
            using Ardalis.SmartEnum;

            namespace {{cnamespace}}
            {
                public sealed partial class {{cname}} {{ctypeStr}} 
                {
                    {{(ctype == GeneratedClassType.Pure ? $"public {cname}(string name, int value) : base(name, value) {{}}" : "")}}

                    static {{cname}}()
                    {
            """;

           string ctemplateEnd = $$"""
                    }
                }
            }
            """;


            var sb = new StringBuilder();
            sb.AppendLine(ctemplateStart);
            // TODO another clean way
            var idx = 1;
            if (ctype == GeneratedClassType.SmartFlagEnum)
            {
                idx = 2;
            }
            for (int i = 0; i < variables.Count; i++)
            {
                sb.AppendLine($"\t\t\t{variables[i].Name} = new {cname}(nameof({variables[i].Name}), {idx});");
                if (ctype == GeneratedClassType.SmartFlagEnum)
                {
                    idx = idx * 2;
                } else
                {
                    idx++;
                }

            }
            sb.AppendLine(ctemplateEnd);
            context.AddSource($"{cname}.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));

        }
       
    }
}