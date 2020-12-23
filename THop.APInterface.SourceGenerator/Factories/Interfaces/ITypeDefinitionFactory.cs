using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.Models.Definitions.TypeDefinitions;

namespace THop.APInterface.SourceGenerator.Factories.Interfaces
{
    public interface ITypeDefinitionFactory
    {
        TypeDefinition CreateTypeDefinitionFromSyntax(TypeDeclarationSyntax typeDeclarationSyntax);
    }
}