using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Services;
using Xunit;

namespace THop.APInterface.SourceGenerator.Test.Services
{
    public class MethodUrlServiceTest
    {
        private readonly MethodUrlService _service;

        public MethodUrlServiceTest()
        {
            _service = new MethodUrlService();
        }

        private static MethodDefinition CreateFunction(string functionName)
        {
            return new MethodDefinition(functionName, "void", new ParameterGenerator[0], new AttributeGenerator[0]);
        }

        private static MethodDefinition CreateFunction(string functionName, string attributeName)
        {
            return new MethodDefinition(functionName, "void", new ParameterGenerator[0],
                new[]
                {
                    new AttributeGenerator(attributeName,
                        new AttributeParameterGenerator[0])
                });
        }

        private MethodDefinition CreateFunction(string functionName, string attributeName,
            string attributeParameterName, object attributeParameterValue)
        {
            return new MethodDefinition(functionName, "void", new ParameterGenerator[0],
                new[]
                {
                    new AttributeGenerator(attributeName,
                        new[] {new AttributeParameterGenerator(attributeParameterName, attributeParameterValue)})
                });
        }

        [Fact]
        public void NoRouteArguments()
        {
            var method = CreateFunction("Foo");

            var result = _service.CreateUrlForMethod(method);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void AttributeWithNoArguments()
        {
            var method = CreateFunction("Bar", "HttpGet");

            var result = _service.CreateUrlForMethod(method);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void AttributeWithStaticRoute()
        {
            var method = CreateFunction("Bar", "HttpGet", "route", "foo/bar");

            var result = _service.CreateUrlForMethod(method);

            Assert.Equal("foo/bar", result);
        }

        [Fact]
        public void NoneHttpAttribute()
        {
            var method = CreateFunction("Foo", "Attribute", "route", "foo/bar");

            var result = _service.CreateUrlForMethod(method);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void MultipleMixedAttribute()
        {

            const string expectedUrlResult = "ipsum";

            var method = new MethodDefinition("FooBar", "void", new ParameterGenerator[0], new[] {new AttributeGenerator("Lorem", new AttributeParameterGenerator[0]), new AttributeGenerator("HttNotFoundHttp", new AttributeParameterGenerator[0]), new AttributeGenerator("HttpPost", new []{new AttributeParameterGenerator("route", expectedUrlResult), }),  } );
            var result = _service.CreateUrlForMethod(method);

            Assert.Equal(expectedUrlResult, result);
        }


    }
}