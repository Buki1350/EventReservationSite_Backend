using System.Text.Json.Serialization;
using DotNetBoilerplate.Api.Events;
using DotNetBoilerplate.Api.Users;
using DotNetBoilerplate.Application;
using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Core;
using DotNetBoilerplate.Infrastructure;
using DotNetBoilerplate.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddShared()
    .AddApplication()
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

var app = builder.Build();

app.MapUsersEndpoints();
app.MapEventsEndpoints();

app.UseInfrastructure();

await app.RunAsync();