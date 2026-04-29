using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DictionaryService.Presentation.EndpointsSetting;

public static class EndpointsExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        IEnumerable<ServiceDescriptor> serviceDescriptors = assembly.DefinedTypes.
            Where(type => type is {IsAbstract: false, IsInterface: false} && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));
        
        services.TryAddEnumerable(serviceDescriptors);
        
        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder buillder = routeGroupBuilder is null ? app : routeGroupBuilder;
        
        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(buillder);
        }
        
        return app;
    }
}