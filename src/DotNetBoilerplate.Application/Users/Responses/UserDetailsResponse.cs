namespace DotNetBoilerplate.Application.Users.Responses;

public class UserDetailsResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}