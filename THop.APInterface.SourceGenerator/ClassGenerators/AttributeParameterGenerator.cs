namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class AttributeParameterGenerator
    {
        public AttributeParameterGenerator(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public object Value { get; }
        public string TextValue => Value.ToString();
    }
}