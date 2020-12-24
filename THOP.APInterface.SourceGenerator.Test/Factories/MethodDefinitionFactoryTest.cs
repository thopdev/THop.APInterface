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
    public class MethodDefinitionFactoryTest
    {
        private SyntaxToken CreateSyntaxIdentifierToken(string name)
        {
            return SyntaxFactory.Identifier(new SyntaxTriviaList(), SyntaxKind.AbstractKeyword, name, name,
                new SyntaxTriviaList());
        }

        private MethodDeclarationSyntax CreateMethod(string name, IEnumerable<string> parameters,
            IEnumerable<string> attributes)
        {
            var returnType = SyntaxFactory.GenericName(SyntaxFactory.Identifier(name));
            return SyntaxFactory.MethodDeclaration(
                new SyntaxList<AttributeListSyntax>().Add(CreateAttributeList(attributes)), new SyntaxTokenList(),
                returnType, null, CreateSyntaxIdentifierToken(name), null,
                SyntaxFactory.ParameterList().AddParameters(parameters
                    .Select(p => SyntaxFactory.Parameter(CreateSyntaxIdentifierToken(p))).ToArray()),
                new SyntaxList<TypeParameterConstraintClauseSyntax>(), null, null, new SyntaxToken());
        }

        private AttributeListSyntax CreateAttributeList(IEnumerable<string> names)
        {
            var attributeList = SyntaxFactory.AttributeList();
            var attributes = names.Select(name =>
                SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(CreateSyntaxIdentifierToken(name)))).ToArray();
            return attributeList.AddAttributes(attributes);
        }

        [Theory]
        [InlineDomainData]
        public void CorrectInput(string name, string[] parameters, string[] attributes,
            [Frozen] Mock<IParameterDefinitionFactory> parameterFactoryMock,
            [Frozen] Mock<IAttributeDefinitionFactory> attributeFactoryMock, MethodDefinitionFactory factory)
        {
            Expression<Func<IParameterDefinitionFactory, ParameterDefinition>> parameterExpression =
                f => f.CreateParameterFromSyntax(It.IsAny<ParameterSyntax>());

            parameterFactoryMock.Setup(parameterExpression).Returns(() => null).Verifiable();

            Expression<Func<IAttributeDefinitionFactory, AttributeDefinition>> attributeExpression =
                f => f.CreateAttributeFromSyntax(It.IsAny<AttributeSyntax>());

            attributeFactoryMock.Setup(attributeExpression).Returns(() => null).Verifiable();


            var x = CreateMethod(name, parameters, attributes);


            var result = factory.CreateMethodFromSyntax(x);

            Assert.NotNull(result);
            Assert.Equal(name, result.Name);

            Assert.Equal(parameters.Length, result.Parameters.Length);
            parameterFactoryMock.Verify(parameterExpression, Times.Exactly(parameters.Length));

            Assert.Equal(attributes.Length, result.Attributes.Length);
            attributeFactoryMock.Verify(attributeExpression, Times.Exactly(attributes.Length));
        }

        [Theory]
        [InlineDomainData]
        public void Exception(string name, MethodDefinitionFactory factory)
        {
            var methodSyntax =
                SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.ArrayType(SyntaxFactory.GenericName(CreateSyntaxIdentifierToken(name))), name);

            Assert.Throws<NotImplementedException>(() => factory.CreateMethodFromSyntax(methodSyntax));
        }
    }
}