namespace DotNetBoilerplate.Api.Users;

internal static class UsersEndpoints
{
    public const string BasePath = "users";
    public const string Tags = "Users";

    public static void MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(BasePath)
            .WithTags(Tags);

        group
            .MapEndpoint<SignUpEndpoint>()
            .MapEndpoint<SignInEndpoint>()
            .MapEndpoint<GetMeEndpoint>()
            .MapEndpoint<UpdateBanStatusEndpoint>();
    }
}