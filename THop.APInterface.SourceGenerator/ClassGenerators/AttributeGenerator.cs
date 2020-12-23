namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class AttributeGenerator
    {
        public AttributeGenerator(string name, AttributeParameterDefinition[] parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public string Name { get;  }
        public AttributeParameterDefinition[] Parameters { get; }
    }
}