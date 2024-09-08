using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Core.Users;

public class User : Entity
{
    private User(UserId id, Email email, Username username, Password password, Role role, DateTime createdAt,
        AccountType accountType)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
        AccountType = accountType;
    }

    private User()
    {
    }

    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public AccountType AccountType { get; private set; }

    public void SetAccountType(AccountType accountType)
    {
        AccountType = accountType;
    }

    public static User New(UserId id, Email email, Username username, Password password, DateTime createdAt)
    {
        return new User(id, email, username, password, Role.User(), createdAt, AccountType.Free());
    }

    public static User NewAdmin(UserId id, Email email, Password password, DateTime createdAt)
    {
        return new User(id, email, "ADMIN", password, Role.Admin(), createdAt, AccountType.Extended());
    }
}