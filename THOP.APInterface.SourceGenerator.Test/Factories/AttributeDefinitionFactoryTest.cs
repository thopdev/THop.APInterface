using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Factories;
using THop.APInterface.SourceGenerator.Factories.Interfaces;
using THop.APInterface.SourceGenerator.Test.Utils.Attributes;
using Xunit;

namespace THop.APInterface.SourceGenerator.Test.Factories
{
    public class AttributeDefinitionFactoryTest
    {
        [Theory]
        [InlineDomainData]
        public void AttributeWithoutParameters(string attributeName, AttributeDefinitionFactory factory)
        {
            var attribute = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attributeName));

            var result = factory.CreateAttributeFromSyntax(attribute);


            Assert.Equal(attributeName, result.Name);
            Assert.Empty(result.Parameters);
        }

        [Theory]
        [InlineDomainData]
        public void ParameterTest(string attributeName, string[] attributeParameters,
            [Frozen] Mock<IAttributeArgumentDefinitionFactory> attributeParameterDefinitionFactoryMock,
            AttributeDefinitionFactory factory)
        {
            Expression<Func<IAttributeArgumentDefinitionFactory, AttributeArgumentDefinition>>
                attributeParameterExpression = f =>
                    f.CreateAttributeParameterFromSyntax(It.IsAny<AttributeArgumentSyntax>());

            attributeParameterDefinitionFactoryMock.Setup(attributeParameterExpression).Returns(() => null)
                .Verifiable();

            var attribute = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attributeName));

            foreach (var attributeParameter in attributeParameters)
            {
                attribute = attribute.AddArgumentListArguments(
                    SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.StringLiteralToken,
                            attributeParameter, attributeParameter, SyntaxFactory.TriviaList()))));
            }

            var result = factory.CreateAttributeFromSyntax(attribute);


            Assert.Equal(attributeName, result.Name);
            Assert.Equal(attributeParameters.Length, result.Parameters.Length);
            attributeParameterDefinitionFactoryMock.Verify(attributeParameterExpression,
                Times.Exactly(attributeParameters.Length));
        }
    }
}