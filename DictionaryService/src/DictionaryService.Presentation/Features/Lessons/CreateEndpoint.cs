using DictionaryService.Presentation.EndpointsSetting;
namespace DictionaryService.Presentation.Features.Lessons;

public class CreateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/lessons", async(CreateHandler handler) =>
        {
            await handler.Handle();
        });
    }
}

public sealed class CreateHandler
{
    public async Task Handle()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
    }
}