using AutoFixture.Xunit2;
using THop.APInterface.SourceGenerator.ClassGenerators;
using THop.APInterface.SourceGenerator.Constants;
using THop.APInterface.SourceGenerator.Models.Definitions.TypeDefinitions;
using THop.APInterface.SourceGenerator.Services;
using THop.APInterface.SourceGenerator.Test.Utils.Attributes;
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
            AttributeParameterDefinition attribute)
        {
            return new ClassDefinition(controllerName, new[]
                {
                    new AttributeGenerator(AttributeConstants.GenerateController,
                        new[] {attribute})
                }, new string[0], new MethodImplementationDefinition[0]
            );
        }


        [Theory]
        [InlineDomainData("IFooBarController", "/foobar")]
        [InlineDomainData("BarFooController", "/barfoo")]
        [InlineDomainData("ILorem", "/lorem")]
        public void NoParameter(string controllerName, string expectedResult, ControllerUrlService service)
        {
            var controller = CreateClassGenerator(controllerName, null);

            var result = service.CreateUrlForController(controller);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineDomainData("ITestController", "[controller]", "/test")]
        [InlineDomainData("IIpsumController", "[controller]/lorem/test", "/ipsum/lorem/test")]
        [InlineDomainData("IIpsumController", "/bar/foo/bar", "/bar/foo/bar")]
        public void Parameter(string controllerName, string attributeValue, string expectedResult, ControllerUrlService service)
        {
            var controller =
                CreateClassGenerator(controllerName, new AttributeParameterDefinition(string.Empty, attributeValue));

            var result = service.CreateUrlForController(controller);
            Assert.Equal(expectedResult, result);
        }
    }
}