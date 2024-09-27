using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.Events;
using DotNetBoilerplate.Api.Reservations;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Application.Events.CreateEvent;
using DotNetBoilerplate.Tests.Integration.setup;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(BoilerplateEndpointsTestsCollection))]
public class ReservationEndpointsTests(BoilerplateEndpointsTestsFixture testsFixture) : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await testsFixture.ResetDbChangesAsync();
    }

    private async Task<AuthenticationHeaderValue> GetAuthenticationHeaderFromNewUserAsync()
    {
        var signUpRequest = new SignUpEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "12345678",
            Username = "username"
        };
        
        await testsFixture.Client.PostAsJsonAsync("users/sign-up", signUpRequest);
        
        var signInRequest = new SignInEndpoint.Request
        {
            Email = "email@t.pl",
            Password = "12345678"
        };
        
        var signInResponse = await testsFixture.Client.PostAsJsonAsync("users/sign-in", signInRequest);
        var signInResult = await signInResponse.Content.ReadFromJsonAsync<SignInEndpoint.Response>();
        return new AuthenticationHeaderValue("Bearer", signInResult.Token);
    }

    [Fact]
    public async Task GivenReservationDataIsCorrect_AndUserIsAuthorized_ReservationCreateShouldSucceed()
    {
        // Arrange
        testsFixture.Client.DefaultRequestHeaders.Authorization =
            await GetAuthenticationHeaderFromNewUserAsync();
        
        var createEventRequest = new CreateEventEndpoint.Request
        {
            Title = "EventTitle",
            Description = "EventDescription",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            Location = "EventLocation",
            MaxNumberOfReservations = 100
        };
        
        var createEventResponse = await testsFixture.Client.PostAsJsonAsync("events/", createEventRequest);
        var createEventResult = await createEventResponse.Content.ReadFromJsonAsync<CreateEventEndpoint.Response>();
        
        var eventId = createEventResult.Id;

        // Act
        var createReservationResponse = await testsFixture.Client.PostAsync($"event/{eventId}/reservations/", null);
        
        // Assert
        createReservationResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GivenEventDoesNotExist_CreateReservation_ShouldReturn404()
    {
        // Arrange
        testsFixture.Client.DefaultRequestHeaders.Authorization =
            await GetAuthenticationHeaderFromNewUserAsync();
        
        var eventId = Guid.NewGuid();

        // Act
        var createReservationResponse = await testsFixture.Client.PostAsync($"event/{eventId}/reservations/", null);
    
        // Assert
        createReservationResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

}
