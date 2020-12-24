using THop.APInterface.SourceGenerator.ClassGenerators;

namespace THop.APInterface.SourceGenerator.Models.Definitions.TypeDefinitions
{
    public abstract class TypeDefinition
    {
        public string Name { get; }
        public AttributeDefinition[] Attributes { get; }
        public string[] Usings { get; }

        protected TypeDefinition(string name, AttributeDefinition[] attributes, string[] usings)
        {
            Name = name;
            Attributes = attributes;
            Usings = usings;
        }
    }
}