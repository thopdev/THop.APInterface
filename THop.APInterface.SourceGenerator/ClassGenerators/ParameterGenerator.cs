namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class ParameterGenerator
    {
        public ParameterGenerator(string name, string type)
        {
            Name = name;
            Type = type;
            Attributes = new AttributeGenerator[0];
        }

        public ParameterGenerator(string name, string type, AttributeGenerator[] attributes)
        {
            Name = name;
            Type = type;
            Attributes = attributes;
        }

        public string Name { get; }
        public string Type { get; }
        public AttributeGenerator[] Attributes { get; }
    }
}