using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using Microsoft.CodeAnalysis;
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
    public class ParameterDefinitionFactoryTest
    {
        private AttributeListSyntax CreateAttributeList(IEnumerable<string> names)
        {
            var attributeList = SyntaxFactory.AttributeList();
            var attributes = names.Select(name =>
                SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(name)))).ToArray();
            return attributeList.AddAttributes(attributes);
        }

        private SyntaxToken CreateSyntaxIdentifierToken(string name)
        {
            return SyntaxFactory.Identifier(new SyntaxTriviaList(), SyntaxKind.AbstractKeyword, name, name,
                new SyntaxTriviaList());
        }

        [Theory]
        [InlineDomainData]
        public void Parameter(string name, string[] attributeNames,
            [Frozen] Mock<IAttributeDefinitionFactory> attributeFactoryMock, ParameterDefinitionFactory factory)
        {
            Expression<Func<IAttributeDefinitionFactory, AttributeDefinition>> attributeExpression =
                f => f.CreateAttributeFromSyntax(It.IsAny<AttributeSyntax>());

            attributeFactoryMock.Setup(attributeExpression).Returns(() => null).Verifiable();

            var attributes = SyntaxFactory.List(new[] {CreateAttributeList(attributeNames)});
            var modifiers = SyntaxFactory.TokenList();

            var syntax = SyntaxFactory.Parameter(attributes, modifiers,
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)),
                CreateSyntaxIdentifierToken(name), null);


            var result = factory.CreateParameterFromSyntax(syntax);

            Assert.Equal(name, result.Name);
            Assert.Equal("string", result.Type);

            Assert.Equal(attributeNames.Length, result.Attributes.Length);
            attributeFactoryMock.Verify(attributeExpression, Times.Exactly(attributeNames.Length));
        }
    }
}