using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;

namespace THop.APInterface.SourceGenerator.Factories.Interfaces
{
    public interface IAttributeArgumentDefinitionFactory
    {
        AttributeArgumentDefinition
            CreateAttributeParameterFromSyntax(AttributeArgumentSyntax attributeArgumentSyntax);
    }
}