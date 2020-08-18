using System;
using ImpromptuInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using THop.APInterface.Dynamic;
using THop.APInterface.Services;

namespace THop.APInterface.Extensions
{
   public static class ServiceProviderExtensions
    {
        public static void AddAPInterface(this IServiceCollection services, Type type)
        {
            services.TryAddScoped(typeof(IHttpClientService), typeof(HttpClientService));
            services.AddScoped(type, sp =>
            {
                var httpClientService = sp.GetRequiredService<IHttpClientService>();
                return new DynamicHttpEndpoint(httpClientService, type).ActLike(type);
            });
        }
    }
}
