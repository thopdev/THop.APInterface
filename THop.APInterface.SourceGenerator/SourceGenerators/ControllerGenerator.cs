using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Factories;
using THop.APInterface.SourceGenerator.Factories.Interfaces;
using THop.APInterface.SourceGenerator.SyntaxReceivers;

namespace THop.APInterface.SourceGenerator.SourceGenerators
{


    [Generator]
    public class ControllerGenerator : ISourceGenerator
    {
        private readonly ITypeDefinitionFactory _typeFactory;

        private readonly IMethodDefinitionFactory _methodFactory;
        private readonly IParameterDefinitionFactory _parameterFactory;

        private readonly IAttributeDefinitionFactory _attributeFactory;
        private readonly IAttributeArgumentDefinitionFactory _argumentFactory;



        public ControllerGenerator()
        {
            _argumentFactory = new AttributeArgumentDefinitionFactory();
            _attributeFactory = new AttributeDefinitionFactory(_argumentFactory);

            _parameterFactory = new ParameterDefinitionFactory(_attributeFactory);
            _methodFactory = new MethodDefinitionFactory(_attributeFactory, _parameterFactory);

            _typeFactory = new TypeDefinitionFactory(_attributeFactory, _methodFactory);
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
            var controllerSyntaxReceiver = context.SyntaxReceiver as ControllerSyntaxReceiver;

            if (controllerSyntaxReceiver?.Candidates != null)
            {
                var types = controllerSyntaxReceiver.Candidates.Select(_typeFactory.CreateTypeDefinitionFromSyntax);
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

        public static ParameterDefinition CreateParametersForFunction(ParameterSyntax parameter)
        {
            return new ParameterDefinition(parameter.Identifier.ValueText, GetTypeNameForTypeSyntax(parameter.Type));
        }

        public static AttributeDefinition CreateAttribute(AttributeSyntax attribute)
        {
            if (attribute.Name is IdentifierNameSyntax identifier)
            {
                return new AttributeDefinition(identifier.Identifier.ValueText, new AttributeArgumentDefinition[0]);
            }

            throw new NotSupportedException($"Type {attribute.Name?.GetType()} is not supported");
        }
    }
}