using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Factories.Interfaces;

namespace THop.APInterface.SourceGenerator.Factories
{
    public class MethodDefinitionFactory : IMethodDefinitionFactory
    {
        private readonly IAttributeDefinitionFactory _attributeDefinitionFactory;
        private readonly IParameterDefinitionFactory _parameterDefinitionFactory;

        public MethodDefinitionFactory(IAttributeDefinitionFactory attributeDefinitionFactory, IParameterDefinitionFactory parameterDefinitionFactory)
        {
            _attributeDefinitionFactory = attributeDefinitionFactory;
            _parameterDefinitionFactory = parameterDefinitionFactory;
        }

        public MethodDefinition CreateMethodFromSyntax(MethodDeclarationSyntax method)
        {
            var name = method.Identifier.ValueText;
            
            if (!(method.ReturnType is GenericNameSyntax genericName))
            {
                throw new NotImplementedException(
                    method.ReturnType.GetType() + " is not yet implemented");
            }

            var returnType = genericName.Identifier.ValueText;

            var parameters =
                method.ParameterList.Parameters.Select(_parameterDefinitionFactory.CreateParameterFromSyntax);

            var attributes = method.AttributeLists.SelectMany(l =>
                l.Attributes.Select(_attributeDefinitionFactory.CreateAttributeFromSyntax));

            return new MethodDefinition(name, returnType, parameters.ToArray(), attributes.ToArray() );
        }
    }
}