using System.Linq;
using THop.APIInterface.SourceGenerator.ClassGenerators;
using THop.APIInterface.SourceGenerator.Constants;
using THop.APIInterface.SourceGenerator.Models.Definitions.TypeDefinitions;

namespace THop.APIInterface.SourceGenerator.Services
{
    public class ControllerUrlService
    {

        public string CreateUrlForController(ClassDefinition controller)
        {
            const string dynamicControllerIdentifier = "[controller]";


            var controllerParameter = controller.Attributes.FirstOrDefault(a => a.Name == AttributeConstants.GenerateController)
                ?.Parameters
                .FirstOrDefault();

            if (controllerParameter != null)
            {
                var url = controllerParameter.TextValue;
                if (url.Contains(dynamicControllerIdentifier))
                {
                    url = url.Replace(dynamicControllerIdentifier, ConvertControllerNameToUrl(controller.Name, true));
                }


                return url;
            }

            return ConvertControllerNameToUrl(controller.Name, true);
        }

        private static string ConvertControllerNameToUrl(string controllerName, bool isInterface)
        {
            const string interfaceIdentifier = "I";
            const string controllerIdentifier = "Controller";

            if (isInterface && controllerName.StartsWith(interfaceIdentifier))
            {
                controllerName = controllerName.Substring(interfaceIdentifier.Length);
            }

            if (controllerName.EndsWith(controllerIdentifier))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - controllerIdentifier.Length);
            }

            return "/" + controllerName.ToLower();
        }
    }
}