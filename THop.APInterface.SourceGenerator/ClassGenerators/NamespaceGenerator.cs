using System.Collections.Generic;
using THop.APInterface.SourceGenerator.Models.Definitions.TypeDefinitions;

namespace THop.APInterface.SourceGenerator.ClassGenerators
{
    public class NamespaceGenerator
    { 
        public IList<ClassDefinition> ClassList { get; set; } = new List<ClassDefinition>();

        public string Namespace { get; set; }
    }
}
