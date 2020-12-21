using THop.APIInterface.SourceGenerator.ClassGenerators;

namespace THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions
{
    public class InterfaceDefinition : TypeDefinition
    {
        public InterfaceDefinition(string name, AttributeGenerator[] attributes, string[] usings, MethodDefinition[] functions) : base(name, attributes, usings)
        {
            Functions = functions;
        }

        public MethodDefinition[] Functions { get; }
    }
}