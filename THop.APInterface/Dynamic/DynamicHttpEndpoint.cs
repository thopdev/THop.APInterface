using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Routing;
using THop.APInterface.Exceptions;
using THop.APInterface.Services;

namespace THop.APInterface.Dynamic
{
    public class DynamicHttpEndpoint : DynamicObject
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _controllerName;
        private readonly Type _type;

        public DynamicHttpEndpoint(IHttpClientService httpClientService, Type type)
        {
            _httpClientService = httpClientService;
            _type = type;
            _controllerName = type.Name.Remove(0, 1).Replace("Endpoint", string.Empty);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methods = _type.GetMethods().Where(methodInfo => methodInfo.Name == binder.Name && args.Length == methodInfo.GetParameters().Length);

            var method = methods.First(methodInfo => args.All(arg => methodInfo.GetParameters().Any(x => arg.GetType() == x.ParameterType)));

            var attribute = method.GetCustomAttribute(typeof(HttpMethodAttribute), true) as HttpMethodAttribute;
            var route = _controllerName + (attribute.Template != null ? "/" + ReplacePlaceHoldersWithVariables(attribute.Template, method, args) : string.Empty);

            var returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault() ?? method.ReturnType;

            var fromQueryModel = method.GetParameters().Where(x => x.GetCustomAttribute<FromQueryAttribute>() != null).ToList();

            if (fromQueryModel.Any())
            {
                route += "?" + string.Join("&", fromQueryModel.Select(parameterInfo =>
                {
                    var param = parameterInfo.ParameterType;
                    var value = args[parameterInfo.Position];
                    if (IsSimple(param))
                    {
                        return $"{parameterInfo.Name}={value}";
                    }

                    return string.Join("&", value.GetType().GetProperties().Select(prop =>
                    {
                        var name = prop.Name;
                        var propValue = prop.GetValue(value);
                        return $"{name}={propValue}";
                    }));
                }));
            }
            switch (attribute)
            {
                case HttpGetAttribute _:
                    var httpGetMethod = _httpClientService.GetType().GetMethod("GetRequestAsync");
                    result = httpGetMethod.MakeGenericMethod(returnType).Invoke(_httpClientService, new object[] { route });
                    return true;

                case HttpPostAttribute _:

                    var bodyParam = method.GetParameters().FirstOrDefault(x => x.GetCustomAttribute<FromBodyAttribute>() != null);
                    if (bodyParam == null)
                    {
                        throw new MissingAttributeException(method, typeof(FromBodyAttribute));
                    }


                    var body = args[bodyParam.Position];
                    var bodyType = body.GetType();

                    var httpPostMethod = _httpClientService.GetType().GetMethod("PostRequestAsync");
                    result = httpPostMethod.MakeGenericMethod(returnType, bodyType).Invoke(_httpClientService, new object[] { route, body });
                    return true;

                case HttpDeleteAttribute _:
                    var httpDeleteMethod = _httpClientService.GetType().GetMethod("DeleteRequestAsync");
                    result = httpDeleteMethod.Invoke(_httpClientService, new object[] { route });
                    return true;
                default:
                    result = default;
                    return false;
            }
        }

        private string ReplacePlaceHoldersWithVariables(string route, MethodBase method, IReadOnlyList<object> args)
        {
            while ((route.IndexOf('{') != -1))
            {
                var indexOfStart = route.IndexOf('{');
                var indexofEnd = route.IndexOf('}');

                var propertyName = route.Substring(indexOfStart + 1, indexofEnd - 1);
                var param = method.GetParameters().First(x => x.Name == propertyName);
                route = route.Replace($"{{{propertyName}}}", args[param.Position].ToString());
            }
            return route;
        }

        private bool IsSimple(Type type)
        {
            return type.IsPrimitive
                   || type.IsEnum
                   || type == typeof(string)
                   || type == typeof(decimal);
        }
    }
}
