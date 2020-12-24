namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class AttributeArgumentDefinition
    {
        public AttributeArgumentDefinition(object value)
        {
            Value = value;
        }

        public object Value { get; }
        public string TextValue => Value.ToString();
    }
}