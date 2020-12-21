
using System;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using THop.APIInterface.SourceGenerator.ClassGenerators;
using THop.APIInterface.SourceGenerator.Factories;
using THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions;
using THop.APInterface.SourceGenerator.Test.Utils.Attributes;
using Xunit;

namespace THop.APInterface.SourceGenerator.Test.Factories
{
    public class ClassDefinitionFactoryTest
    {
        private SyntaxToken CreateSyntaxIdentifierToken(string name)
        {
            return SyntaxFactory.Identifier(new SyntaxTriviaList(), SyntaxKind.AbstractKeyword, name, name,
                new SyntaxTriviaList());
        }

        private MethodDeclarationSyntax CreateMethod(string name)
        {
            var returnType = SyntaxFactory.GenericName(SyntaxFactory.Identifier(name));
            return SyntaxFactory.MethodDeclaration(new SyntaxList<AttributeListSyntax>(), new SyntaxTokenList(),
                returnType, null, CreateSyntaxIdentifierToken(name), null, SyntaxFactory.ParameterList(), new SyntaxList<TypeParameterConstraintClauseSyntax>(), null, null, new SyntaxToken());
        }

        private AttributeListSyntax CreateAttributeList(string[] names)
        {
            var attributeList = SyntaxFactory.AttributeList();
            var attributes = names.Select(name => SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(CreateSyntaxIdentifierToken(name)))).ToArray();
            return attributeList.AddAttributes(attributes);
        }

        [Theory, AutoMoqData]
        public void ClassDefinition(string className, ClassDefinitionFactory factory)
        {
            var syntaxToken = CreateSyntaxIdentifierToken(className);
            var classDeclaration = SyntaxFactory.ClassDeclaration(new SyntaxList<AttributeListSyntax>(), new SyntaxTokenList(),
                syntaxToken, null, null, new SyntaxList<TypeParameterConstraintClauseSyntax>(), new SyntaxList<MemberDeclarationSyntax>() );
            
            Assert.Throws<NotImplementedException>(() => factory.CreateTypeDefinitionFromSyntax(classDeclaration));
        }

        [Theory, AutoMoqData]
        public void InterfaceDefinition(string interfaceName, string[] methodNames,  string[] attributeNames, [Frozen] Mock<IMethodDefinitionFactory> methodDefinitionFactoryMock, [Frozen] Mock<IAttributeDefinitionFactory> attributeDefinitionFactoryMock,  ClassDefinitionFactory factory)
        {
            Expression<Func<IMethodDefinitionFactory, MethodDefinition>> createMethodExpression = e => e.CreateMethodFromSyntax(It.IsAny<MethodDeclarationSyntax>());
            Expression<Func<IAttributeDefinitionFactory, AttributeGenerator>> createAttributeExpression = e => e.CreateAttributeFromSyntax(It.IsAny<AttributeSyntax>());

            var syntaxToken = SyntaxFactory.Identifier(new SyntaxTriviaList(), SyntaxKind.AbstractKeyword, interfaceName, interfaceName,
                new SyntaxTriviaList());

            var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration(new SyntaxList<AttributeListSyntax>(), new SyntaxTokenList(),
                syntaxToken, null, null, new SyntaxList<TypeParameterConstraintClauseSyntax>(), new SyntaxList<MemberDeclarationSyntax>()).AddMembers(methodNames.Select(CreateMethod).ToArray()).AddAttributeLists(CreateAttributeList(attributeNames));

           methodDefinitionFactoryMock.Setup(createMethodExpression).Returns(() => null).Verifiable();
           attributeDefinitionFactoryMock.Setup(createAttributeExpression).Returns(() => null).Verifiable();


            var result = factory.CreateTypeDefinitionFromSyntax(interfaceDeclaration);

            var interfaceDefinition =  Assert.IsType<InterfaceDefinition>(result);
            Assert.Equal(interfaceName, interfaceDefinition.Name);
            Assert.Empty(interfaceDefinition.Usings);

            Assert.Equal(methodNames.Length, interfaceDefinition.Functions.Length);
            Assert.Equal(attributeNames.Length, interfaceDefinition.Attributes.Length);

            methodDefinitionFactoryMock.Verify(createMethodExpression, Times.Exactly(methodNames.Length));
            attributeDefinitionFactoryMock.Verify(createAttributeExpression, Times.Exactly(attributeNames.Length));
        }
    }
}
