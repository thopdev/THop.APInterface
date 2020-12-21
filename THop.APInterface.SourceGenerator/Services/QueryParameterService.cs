using System.Collections.Generic;
using System.Linq;
using THop.APInterface.SourceGenerator.ClassGenerators;

namespace THop.APInterface.SourceGenerator.Services
{
    public class QueryParameterService
    {
        public string CreateQueryStringFromParameters(IEnumerable<ParameterGenerator> fromQueryParameters)
        {
            var result = string.Join("&", fromQueryParameters.Select(CreateQueryStringFromParameter));

            return string.IsNullOrEmpty(result) ? string.Empty : "?" + result;
        }

        private static string CreateQueryStringFromParameter(ParameterGenerator fromQueryParameter)
        {
            return $"{fromQueryParameter.Name}={{{fromQueryParameter.Name}}}";
        }
    }
}