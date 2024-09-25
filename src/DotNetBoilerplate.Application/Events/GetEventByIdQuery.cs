using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Events;

public sealed record GetEventByIdQuery(Guid EventId) : IQuery<GetEventByIdResponse?>;