﻿using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Events;

public sealed record GetAllEventsQuery() : IQuery<GetAllEventsResponse>
{
}