using DictionaryService.Presentation.EndpointsSetting;

namespace DictionaryService.Presentation.Configuration;

public static class AppExtensions
{
    public static IApplicationBuilder Configure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        RouteGroupBuilder apiGroup = app.MapGroup("/api/lessons").WithOpenApi();
        app.MapEndpoints(apiGroup);
        
        return app;
    }
}