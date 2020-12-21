using THop.APIInterface.SourceGenerator.ClassGenerators;

namespace THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions
{
    public class ClassDefinition : TypeDefinition
    {
        public ClassDefinition(string name, AttributeGenerator[] attributes, string[] usings, MethodImplementationDefinition[] functions) : base(name, attributes, usings)
        {
            Functions = functions;
        }

        public MethodImplementationDefinition[] Functions { get;  }
    }
}