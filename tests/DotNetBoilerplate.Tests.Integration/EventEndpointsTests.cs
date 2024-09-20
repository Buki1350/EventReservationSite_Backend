using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DotNetBoilerplate.Api.Events;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Application.Events.CreateEvent;
using DotNetBoilerplate.Tests.Integration.setup;
using Microsoft.AspNetCore.Http.HttpResults;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration;

[Collection(nameof(BoilerplateEndpointsTestsCollection))]
public class EventEndpointsTests(BoilerplateEndpointsTestsFixture testsFixture) : IAsyncLifetime
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
    public async Task GivenEventDataIsCorrect_AndUserIsAuthorized_EventCreateShouldSucceed()
    {
        //Arrange
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

        testsFixture.Client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", signInResult.Token);
        
        var createEventRequest = new CreateEventEndpoint.Request
        {
            Title = "EventTitle",
            Description = "EventDescription",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            Location = "EventLocation",
            MaxNumberOfReservations = 100
        };
        
        //Act
        var createEventResponse = await testsFixture.Client.PostAsJsonAsync("events/createEvent", createEventRequest);
        
        //Assert
        createEventResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

    }

    [Fact]
    public async Task GivenEventDataIsCorrect_AndUserIsAuthorized_EventUpdateShouldSucceed()
    {
        //Arrange
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

        testsFixture.Client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", signInResult.Token);
        
        var createEventRequest = new CreateEventEndpoint.Request
        {
            Title = "EventTitle",
            Description = "EventDescription",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            Location = "EventLocation",
            MaxNumberOfReservations = 100
        };
        
        var createEventResponse = await testsFixture.Client.PostAsJsonAsync("events/createEvent", createEventRequest);
        var createEventResult = await createEventResponse.Content.ReadFromJsonAsync<CreateEventEndpoint.Response>();
        var eventId = createEventResult.Id;
        
            //UpdateEndpointRequest
        var updateEventRequest = new UpdateEventEndpoint.Request
        {
            Id = eventId,
            NewTitle = "NewTitle",
            NewDescription = "NewDescription",
            NewStartDate = DateTime.Now.AddDays(2),
            NewEndDate = DateTime.Now.AddDays(4),
            NewLocation = "NewLocation",
            NewMaxNumberOfReservations = 200
        };
        
        
        //Act
        var updateEventResponse = await testsFixture.Client.PostAsJsonAsync("events/updateEvent", updateEventRequest);
        updateEventResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAllEventsEndpoints_ShouldReturnAllEvents()
    {
        //Arrange
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

        testsFixture.Client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", signInResult.Token);
        
        var createEventRequest1 = new CreateEventEndpoint.Request
        {
            Title = "EventTitle1",
            Description = "EventDescription1",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            Location = "EventLocation1",
            MaxNumberOfReservations = 100
        };
        
        var createEventRequest2 = new CreateEventEndpoint.Request
        {
            Title = "EventTitle2",
            Description = "EventDescription2",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            Location = "EventLocation2",
            MaxNumberOfReservations = 100
        };
        
        var createEventRequest3 = new CreateEventEndpoint.Request
        {
            Title = "EventTitle3",
            Description = "EventDescription3",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            Location = "EventLocation3",
            MaxNumberOfReservations = 100
        };
        
        var createEventResponse1 = await testsFixture.Client.PostAsJsonAsync("events/createEvent", createEventRequest1);
        await createEventResponse1.Content.ReadFromJsonAsync<CreateEventEndpoint.Response>();
        
        var createEventResponse2 = await testsFixture.Client.PostAsJsonAsync("events/createEvent", createEventRequest2);
        await createEventResponse2.Content.ReadFromJsonAsync<CreateEventEndpoint.Response>();
        
        var createEventResponse3 = await testsFixture.Client.PostAsJsonAsync("events/createEvent", createEventRequest3);
        await createEventResponse3.Content.ReadFromJsonAsync<CreateEventEndpoint.Response>();
        
        //Act
        var getEventsResponse = await testsFixture.Client.GetAsync("events/");
        
        //Assert
        getEventsResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}