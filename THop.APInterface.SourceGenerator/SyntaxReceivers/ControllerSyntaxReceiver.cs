using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.Constants;

namespace THop.APInterface.SourceGenerator.SyntaxReceivers
{
    public class ControllerSyntaxReceiver : ISyntaxReceiver
    {
        public List<InterfaceDeclarationSyntax> Candidates { get; } =
            new List<InterfaceDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is InterfaceDeclarationSyntax typeDeclarationSyntax)
            {
                foreach (var attributeList in
                    typeDeclarationSyntax.AttributeLists)
                {
                    foreach (var attribute in attributeList.Attributes)
                    {
                        if (attribute.Name.ToString() == AttributeConstants.GenerateController)
                        {
                            Candidates.Add(typeDeclarationSyntax);
                        }
                    }
                }
            }
        }
    }
}
