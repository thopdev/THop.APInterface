using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Factories.Interfaces;

namespace THop.APInterface.SourceGenerator.Factories
{
    public class AttributeArgumentDefinitionFactory : IAttributeArgumentDefinitionFactory
    {
        public AttributeArgumentDefinition CreateAttributeParameterFromSyntax(AttributeArgumentSyntax attributeArgumentSyntax)
        {
            if (attributeArgumentSyntax.Expression is LiteralExpressionSyntax literalExpression)
            {
                return new AttributeArgumentDefinition(literalExpression.Token.ValueText);
            }

            throw new NotImplementedException(
                attributeArgumentSyntax.GetType() + " is not yet implemented");
        }
    }
}
