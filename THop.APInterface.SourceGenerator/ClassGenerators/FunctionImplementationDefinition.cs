
namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class MethodDefinition
    {
        public MethodDefinition(string name, string returnType, ParameterGenerator[] parameters, AttributeGenerator[] attributes)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = parameters;
            Attributes = attributes;
        }

        public MethodDefinition()
        {
            
        }
        public string Name { get; }
        public string ReturnType { get; }
        public ParameterGenerator[] Parameters { get; }
        public AttributeGenerator[] Attributes { get; }

    }

    public class MethodImplementationDefinition : MethodDefinition
    {
        public MethodImplementationDefinition(string name, string returnType, ParameterGenerator[] parameters, AttributeGenerator[] attributes, string[] body) : base(name, returnType, parameters, attributes)
        {
            Body = body;
        }

        public string[] Body { get; }

    }
}