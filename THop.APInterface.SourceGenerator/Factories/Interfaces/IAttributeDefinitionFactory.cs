using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;

namespace THop.APInterface.SourceGenerator.Factories.Interfaces
{
    public interface IAttributeDefinitionFactory
    {
        AttributeGenerator CreateAttributeFromSyntax(AttributeSyntax attributeSyntax);
    }
}