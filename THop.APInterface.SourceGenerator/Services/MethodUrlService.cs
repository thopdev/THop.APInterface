using System.Linq;
using THop.APIInterface.SourceGenerator.ClassGenerators;

namespace THop.APIInterface.SourceGenerator.Services
{
    public class MethodUrlService
    {
        public string CreateUrlForMethod(MethodDefinition method)
        {
            var httpAttribute = method.Attributes.FirstOrDefault(a => a.Name.StartsWith("Http"));
            if (httpAttribute != null && httpAttribute.Parameters.Any())
                return httpAttribute.Parameters.First().TextValue;

            return string.Empty;
        }
    }
}