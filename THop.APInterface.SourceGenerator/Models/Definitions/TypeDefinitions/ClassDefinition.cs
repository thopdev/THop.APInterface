using THop.APInterface.SourceGenerator.ClassGenerators;

namespace THop.APInterface.SourceGenerator.Models.Definitions.TypeDefinitions
{
    public class ClassDefinition : TypeDefinition
    {
        public ClassDefinition(string name, AttributeDefinition[] attributes, string[] usings, MethodImplementationDefinition[] functions) : base(name, attributes, usings)
        {
            Functions = functions;
        }

        public MethodImplementationDefinition[] Functions { get;  }
    }
}