namespace THop.APIInterface.SourceGenerator.ClassGenerators
{
    public class AttributeGenerator
    {
        public AttributeGenerator(string name, AttributeParameterGenerator[] parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public string Name { get;  }
        public AttributeParameterGenerator[] Parameters { get; }
    }
}