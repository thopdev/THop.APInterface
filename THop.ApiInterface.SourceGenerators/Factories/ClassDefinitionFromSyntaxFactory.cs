using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APIInterface.SourceGenerator.ClassGenerators;
using THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions;

namespace THop.APIInterface.SourceGenerator.Factories
{
    public class ClassDefinitionFactory
    {
        private readonly IAttributeDefinitionFactory _attributeDefinitionFactory;
        private readonly IMethodDefinitionFactory _methodDefinitionFactory;

        public ClassDefinitionFactory(IAttributeDefinitionFactory attributeDefinitionFactory,
            IMethodDefinitionFactory methodDefinitionFactory)
        {
            _attributeDefinitionFactory = attributeDefinitionFactory;
            _methodDefinitionFactory = methodDefinitionFactory;
        }

        public TypeDefinition CreateTypeDefinitionFromSyntax(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax switch
            {
                InterfaceDeclarationSyntax interfaceDeclarationSyntax => InterfaceDefinitionFromSyntax(interfaceDeclarationSyntax),

                ClassDeclarationSyntax _ => throw new NotImplementedException(
                    "ClassDeclaration is not yet implemented "),

                _ => throw new NotImplementedException(
                    $"{typeDeclarationSyntax.GetType()} is not yet implemented for CreateTypeDefinitionFromSyntax")
            };
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private InterfaceDefinition InterfaceDefinitionFromSyntax(InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            var typeName = interfaceDeclarationSyntax.Identifier.ValueText;
            var attributes = interfaceDeclarationSyntax.AttributeLists.FirstOrDefault()?.Attributes
                .Select(_attributeDefinitionFactory.CreateAttributeFromSyntax).ToArray() ?? new AttributeGenerator[0];

            var members = interfaceDeclarationSyntax.Members.OfType<MethodDeclarationSyntax>()
                .Select(member => _methodDefinitionFactory.CreateMethodFromSyntax(member)).ToArray();

            return new InterfaceDefinition(typeName, attributes, new string[0], members);
        }
    }

    public interface IAttributeDefinitionFactory
    {
        AttributeGenerator CreateAttributeFromSyntax(AttributeSyntax attributeSyntax);
    }

    public interface IMethodDefinitionFactory
    {
        MethodDefinition CreateMethodFromSyntax(MethodDeclarationSyntax method);
    }
}