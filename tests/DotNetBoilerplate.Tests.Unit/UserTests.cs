using DotNetBoilerplate.Core.Users;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Unit;

public class UserTests
{
    [Fact]
    public void GivenUserIsNotBanned_BanAtShouldNotBeNull()
    {
        //Arange
        var user = User.New(Guid.NewGuid(), "email@email.com", "password", "username", DateTime.Now);
        user.UpdateIsBanned(false, DateTime.Now);
        
        //Act
        user.UpdateIsBanned(true, DateTime.Now);
        
        //Assert
        user.BannedAt.ShouldNotBeNull();
        
    }

    [Fact]
    public void GivenUserIsAlreadyBanned_AndShouldBeBannedIsFalse_BannedAtShouldBeNull()
    {
        //Arange
        var user = User.New(Guid.NewGuid(), "email@email.com", "password", "username", DateTime.Now);
        user.UpdateIsBanned(true, DateTime.Now);
        
        //Act
        user.UpdateIsBanned(false, DateTime.Now);
        
        //Assert
        user.BannedAt.ShouldBeNull();
        
    }
}