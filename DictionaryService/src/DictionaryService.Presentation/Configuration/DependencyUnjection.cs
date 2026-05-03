using System.Reflection;
using DictionaryService.Presentation.EndpointsSetting;
using Microsoft.OpenApi;

namespace DictionaryService.Presentation.Configuration;

public static class DependencyUnjection
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddOpenApiSpec().AddEndpoints(typeof(Program).Assembly);;
    }
    
    private static IServiceCollection AddOpenApiSpec(this IServiceCollection services)
    {
        services.AddOpenApi();

        services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Education Content Service",
            Version = "v1",
            Contact = new OpenApiContact{
                Name = "Vadim", Email = "asdfghjkl"
            }
        }));
        
        return services;
    }
}