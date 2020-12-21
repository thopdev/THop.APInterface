using System.Collections.Generic;
using System.Linq;
using System.Text;
using THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions;

namespace THop.APIInterface.SourceGenerator.ClassGenerators
{
    public class NamespaceGenerator
    { 
        public IList<ClassDefinition> ClassList { get; set; } = new List<ClassDefinition>();

        public string Namespace { get; set; }
    }
}
