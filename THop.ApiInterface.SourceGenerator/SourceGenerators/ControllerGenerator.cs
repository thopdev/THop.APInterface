using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APIInterface.SourceGenerator.ClassGenerators;
using THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions;
using THop.APIInterface.SourceGenerator.Services;
using THop.APIInterface.SourceGenerator.SyntaxReceivers;

namespace THop.APIInterface.SourceGenerator.SourceGenerators
{


    [Generator]
    public class ControllerGenerator : ISourceGenerator
    {

        private QueryParameterService _parameterService;

        public ControllerGenerator()
        {
            _parameterService = new QueryParameterService();
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ControllerSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached) Debugger.Launch();
#endif
            var x = context.SyntaxReceiver as ControllerSyntaxReceiver;

            if (x?.Candidates != null)
                foreach (var candidate in x.Candidates)
                {
                    // var classGenerator =
                    //     ControllerClassFactory.CreateControllerClassFromInterfaceDeclaration(candidate.Identifier
                    //         .ValueText);
                    //
                    // foreach (var member in candidate.Members.OfType<MethodDeclarationSyntax>())
                    // {
                    //     var httpAttribute = member.AttributeLists.Select(attributeSyntax =>
                    //             attributeSyntax.Attributes.FirstOrDefault(attr =>
                    //                 (attr.Name as IdentifierNameSyntax)?.Identifier.ValueText.StartsWith("Http") ??
                    //                 false))
                    //         .FirstOrDefault();
                    //
                    //     if (httpAttribute != null)
                    //     {
                    //
                    //
                    //     }
                    }
                

            Debug.WriteLine("Initialize code generator");

            // context.AddSource("TestController.cs", "namespace THop\r\n{ \n public class TestController {}} \n ");
        }
    }


    public static class ControllerClassFactory
    {

        

        private static string GetTypeNameForTypeSyntax(TypeSyntax type)
        {
            return type switch
            {
                IdentifierNameSyntax identifierName => identifierName.Identifier.ValueText,
                PredefinedTypeSyntax predefinedType => predefinedType.Keyword.ValueText,
                _ => throw new NotSupportedException($"Type {type?.GetType()} is not supported")
            };
        }

        public static ParameterGenerator CreateParametersForFunction(ParameterSyntax parameter)
        {
            return new ParameterGenerator(parameter.Identifier.ValueText, GetTypeNameForTypeSyntax(parameter.Type));
        }

        public static AttributeGenerator CreateAttribute(AttributeSyntax attribute)
        {
            if (attribute.Name is IdentifierNameSyntax identifier)
            {
                return new AttributeGenerator(identifier.Identifier.ValueText, new AttributeParameterGenerator[0]);
            }

            throw new NotSupportedException($"Type {attribute.Name?.GetType()} is not supported");
        }
    }
}