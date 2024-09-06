using DotNetBoilerplate.Application.Users.Responses;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Users;

public class BrowseUsers : PagedQuery<UserDetailsResponse>
{
    public string Username { get; set; }
}