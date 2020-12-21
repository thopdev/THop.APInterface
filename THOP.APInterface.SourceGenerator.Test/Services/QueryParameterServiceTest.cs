using System.Collections.Generic;
using System.Linq;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Services;
using Xunit;

namespace THop.APInterface.SourceGenerator.Test.Services
{
    public class QueryParameterServiceTest
    {
        private static IEnumerable<ParameterGenerator> CreateParameterGenerators(params string[] paramStrings)
        {
            return paramStrings.Select(p => new ParameterGenerator (p, p));
        }

        private readonly QueryParameterService _service;

        public QueryParameterServiceTest()
        {
            _service = new QueryParameterService();
        }

        [Fact]
        public void NoParameter()
        {
            var result = _service.CreateQueryStringFromParameters(CreateParameterGenerators());

            Assert.Empty(result);
        }

        [Fact]
        public void SingleParameter()
        {
            var result = _service.CreateQueryStringFromParameters(CreateParameterGenerators("Lorem"));

            Assert.Equal("?Lorem={Lorem}", result);
        }

        [Fact]
        public void MultipleParameters()
        {
            var result = _service.CreateQueryStringFromParameters(CreateParameterGenerators("Foo", "Bar", "Lorem"));

            Assert.Equal("?Foo={Foo}&Bar={Bar}&Lorem={Lorem}", result);
        }
    }
}