namespace DotNetBoilerplate.Api.Reservations;

internal static class ReservationsEndpoints
{
    public const string BasePath = "";
    public const string Tags = "Reservations";

    public static void MapReservationsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<CreateReservationEndpoint>();
    }
}