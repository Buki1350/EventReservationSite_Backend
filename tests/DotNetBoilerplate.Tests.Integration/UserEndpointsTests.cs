using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Application.Users.Responses;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Tests.Integration.setup;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

[Collection(nameof(BoilerplateEndpointsTestsCollection))]
public class UsersEndpointsTests(BoilerplateEndpointsTestsFixture testsFixture) : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await testsFixture.ResetDbChangesAsync();
    }

    [Fact]
    public async Task GivenEmailIsUnique_SignUp_ShouldSucceed()
    {
        /*await using var scope = testsFixture.ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetService<DotNetBoilerplateWriteDbContext>();

        dbContext.Users.Add(User.New(Guid.NewGuid(), "email@t.pl", "ttttttt", "ttttttt", DateTime.Now));

        await dbContext.SaveChangesAsync();
        */

        //Arrange
        var request = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "ttttttt",
            Username = "username"
        };

        //Act
        var response = await testsFixture.Client.PostAsJsonAsync("users/sign-up", request);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GivenLoginDataMatchThoseInDatabase_SignIn_ShouldSucceed()
    {
        
        //Arrange
        var signUprequest = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "12345678",
            Username = "username"
        };
        
        await testsFixture.Client.PostAsJsonAsync("users/sign-up", signUprequest);
        
        var signInRequest = new SignInEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "12345678"
        };

        //Act
        var signInResponse = await testsFixture.Client.PostAsJsonAsync("users/sign-in", signInRequest);
        var signInResult = await signInResponse.Content.ReadFromJsonAsync<SignInEndpoint.Response>();

        //Assert
        signInResult.ShouldNotBeNull();
        signInResult.ShouldBeOfType<SignInEndpoint.Response>();
        signInResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMeEndpoint_ShouldReturn_CurrentUserInfo()
    {
        //Arrange
        var request = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "12345678",
            Username = "username"
        };
        
        await testsFixture.Client.PostAsJsonAsync("users/sign-up", request);
        
        var signInRequest = new SignInEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "12345678"
        };

        var signInResponse = await testsFixture.Client.PostAsJsonAsync("users/sign-in", signInRequest);
        var signInResult = await signInResponse.Content.ReadFromJsonAsync<SignInEndpoint.Response>();
        
        testsFixture.Client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", signInResult.Token);

        //Act
        var getMeResponse = await testsFixture.Client.GetAsync("users/me");
        
        //Assert
        getMeResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var userInfo = await getMeResponse.Content.ReadFromJsonAsync<UserDetailsResponse>();
        userInfo.ShouldNotBeNull();
        userInfo.Email.ShouldBe("email@t.pl");
        userInfo.Username.ShouldBe("username");
    }
    
}