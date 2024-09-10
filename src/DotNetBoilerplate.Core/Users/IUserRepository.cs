namespace DotNetBoilerplate.Core.Users;

public interface IUserRepository
{
    Task<User> FindByIdAsync(UserId id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}