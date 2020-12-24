namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class ParameterDefinition
    {
        public ParameterDefinition(string name, string type)
        {
            Name = name;
            Type = type;
            Attributes = new AttributeDefinition[0];
        }

        public ParameterDefinition(string name, string type, AttributeDefinition[] attributes)
        {
            Name = name;
            Type = type;
            Attributes = attributes;
        }

        public string Name { get; }
        public string Type { get; }
        public AttributeDefinition[] Attributes { get; }
    }
}