
namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class MethodDefinition
    {
        public MethodDefinition(string name, string returnType, ParameterDefinition[] parameters, AttributeDefinition[] attributes)
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
        public ParameterDefinition[] Parameters { get; }
        public AttributeDefinition[] Attributes { get; }

    }

    public class MethodImplementationDefinition : MethodDefinition
    {
        public MethodImplementationDefinition(string name, string returnType, ParameterDefinition[] parameters, AttributeDefinition[] attributes, string[] body) : base(name, returnType, parameters, attributes)
        {
            Body = body;
        }

        public string[] Body { get; }

    }
}