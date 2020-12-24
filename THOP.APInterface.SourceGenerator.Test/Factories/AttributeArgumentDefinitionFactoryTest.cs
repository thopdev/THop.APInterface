using System;
using Microsoft.CodeAnalysis.CSharp;
using THop.APInterface.SourceGenerator.Factories;
using THop.APInterface.SourceGenerator.Test.Utils.Attributes;
using Xunit;

namespace THop.APInterface.SourceGenerator.Test.Factories
{
    public class AttributeArgumentDefinitionFactoryTest
    {
        [Theory]
        [InlineDomainData]
        public void CreateArgument(string text, AttributeArgumentDefinitionFactory factory)
        {
            var argument = SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.StringLiteralToken,
                    text, text, SyntaxFactory.TriviaList())));

            var result = factory.CreateAttributeParameterFromSyntax(argument);

            Assert.NotNull(result);
            Assert.Equal(text, result.TextValue);
        }

        [Theory]
        [InlineDomainData]
        public void NotImplemented(AttributeArgumentDefinitionFactory factory)
        {
            var argument = SyntaxFactory.AttributeArgument(SyntaxFactory.BaseExpression());

            Assert.Throws<NotImplementedException>(() => factory.CreateAttributeParameterFromSyntax(argument));
        }
    }
}