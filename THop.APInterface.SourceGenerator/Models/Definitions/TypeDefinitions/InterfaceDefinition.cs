using THop.APInterface.SourceGenerator.ClassGenerators;

namespace THop.APInterface.SourceGenerator.Models.Definitions.TypeDefinitions
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