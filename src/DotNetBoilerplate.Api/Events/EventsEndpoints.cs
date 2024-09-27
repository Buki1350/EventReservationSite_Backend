using DotNetBoilerplate.Application.Events.Responses;

namespace DotNetBoilerplate.Api.Events;

internal static class EventsEndpoints
{
    public const string BasePath = "events";
    public const string Tags = "Events";

    public static void MapEventsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<CreateEventEndpoint>()
            .MapEndpoint<UpdateEventEndpoint>()
            .MapEndpoint<GetAllEventsEndpoint>()
            .MapEndpoint<GetEventByIdEndpoint>()
            .MapEndpoint<GetAllEventsWithDetailsEndpoint>();
    }
}