using THop.APIInterface.SourceGenerator.ClassGenerators;

namespace THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions
{
    public abstract class TypeDefinition
    {
        public string Name { get; }
        public AttributeGenerator[] Attributes { get; }
        public string[] Usings { get; }

        protected TypeDefinition(string name, AttributeGenerator[] attributes, string[] usings)
        {
            Name = name;
            Attributes = attributes;
            Usings = usings;
        }
    }
}