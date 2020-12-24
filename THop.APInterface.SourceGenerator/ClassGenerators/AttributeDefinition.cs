namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class AttributeDefinition
    {
        public AttributeDefinition(string name, AttributeArgumentDefinition[] parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public string Name { get;  }
        public AttributeArgumentDefinition[] Parameters { get; }
    }
}