using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Factories.Interfaces;

namespace THop.APInterface.SourceGenerator.Factories
{
    public class AttributeDefinitionFactory : IAttributeDefinitionFactory
    {
        private readonly IAttributeArgumentDefinitionFactory _attributeArgumentDefinitionFactory;

        public AttributeDefinitionFactory(IAttributeArgumentDefinitionFactory attributeArgumentDefinitionFactory)
        {
            _attributeArgumentDefinitionFactory = attributeArgumentDefinitionFactory;
        }

        public AttributeDefinition CreateAttributeFromSyntax(AttributeSyntax attributeSyntax)
        {
            if (!(attributeSyntax.Name is IdentifierNameSyntax nameSyntax))
            {
                throw new NotImplementedException(attributeSyntax.Name.GetType() + " is not yet implemented");
            }

            var parameters =
                attributeSyntax.ArgumentList?.Arguments.Select(_attributeArgumentDefinitionFactory
                    .CreateAttributeParameterFromSyntax).ToArray() ?? new AttributeArgumentDefinition[0];

            var name = nameSyntax.Identifier.ValueText;
            return new AttributeDefinition(name,  parameters);

        }
    }
}