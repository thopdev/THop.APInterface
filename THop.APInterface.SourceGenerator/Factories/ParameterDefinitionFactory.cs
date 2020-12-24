using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Factories.Interfaces;

namespace THop.APInterface.SourceGenerator.Factories
{
    public class ParameterDefinitionFactory : IParameterDefinitionFactory
    {
        private readonly IAttributeDefinitionFactory _attributeDefinitionFactory;

        public ParameterDefinitionFactory(IAttributeDefinitionFactory attributeDefinitionFactory)
        {
            _attributeDefinitionFactory = attributeDefinitionFactory;
        }

        public ParameterDefinition CreateParameterFromSyntax(ParameterSyntax parameterSyntax)
        {
            var name = parameterSyntax.Identifier.ValueText;
            var returnType = parameterSyntax.Type switch
            {
                PredefinedTypeSyntax predefinedType => predefinedType.Keyword.ValueText,
                RefTypeSyntax typeDeclaration => typeDeclaration.RefKeyword.ValueText,
                _ => throw new NotSupportedException(parameterSyntax.Type?.GetType() + " is not yet implemented")
            };

            var attributes =
                parameterSyntax.AttributeLists.SelectMany(l =>
                    l.Attributes.Select(_attributeDefinitionFactory.CreateAttributeFromSyntax));

            return new ParameterDefinition(name, returnType, attributes.ToArray());
        }
    }
}