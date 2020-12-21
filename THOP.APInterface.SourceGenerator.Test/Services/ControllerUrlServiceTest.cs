using THop.APIInterface.SourceGenerator.ClassGenerators;
using THop.APIInterface.SourceGenerator.Constants;
using THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions;
using THop.APIInterface.SourceGenerator.Services;
using Xunit;

namespace THop.APInterface.SourceGenerator.Test.Services
{
    public class ControllerUrlServiceTest
    {
        private readonly ControllerUrlService _service;

        public ControllerUrlServiceTest()
        {
            _service = new ControllerUrlService();
        }

        private static ClassDefinition CreateClassGenerator(string controllerName,
            AttributeParameterGenerator attribute)
        {
            return new ClassDefinition(controllerName, new[]
                {
                    new AttributeGenerator(AttributeConstants.GenerateController,
                        new[] {attribute})
                }, new string[0], new MethodImplementationDefinition[0]
            );
        }


        [Fact]
        public void InterfaceControllerWithoutParameter()
        {
            var controller = CreateClassGenerator("IFooBarController", null);

            var result = _service.CreateUrlForController(controller);
            Assert.Equal("/foobar", result);
        }

        [Fact]
        public void ControllerWithoutParameter()
        {
            var controller = CreateClassGenerator("BarFooController", null);

            var result = _service.CreateUrlForController(controller);
            Assert.Equal("/barfoo", result);
        }

        [Fact]
        public void InterfaceWithoutParameter()
        {
            var controller = CreateClassGenerator("ILorem", null);

            var result = _service.CreateUrlForController(controller);
            Assert.Equal("/lorem", result);
        }

        [Fact]
        public void ParameterOnlyDynamicControllerName()
        {
            var controller =
                CreateClassGenerator("ITestController", new AttributeParameterGenerator("", "[controller]"));

            var result = _service.CreateUrlForController(controller);
            Assert.Equal("/test", result);
        }

        [Fact]
        public void ParameterWithDynamicControllerNameAndRoute()
        {
            var controller = CreateClassGenerator("IIpsumController",
                new AttributeParameterGenerator("", "[controller]/lorem/test"));

            var result = _service.CreateUrlForController(controller);
            Assert.Equal("/ipsum/lorem/test", result);
        }

        [Fact]
        public void RouteOnly()
        {
            var controller =
                CreateClassGenerator("IIpsumController", new AttributeParameterGenerator("", "/bar/foo/bar"));

            var result = _service.CreateUrlForController(controller);
            Assert.Equal("/bar/foo/bar", result);
        }
    }
}