using DotNetBoilerplate.Application.Events.DTOs;

namespace DotNetBoilerplate.Application.Events.Responses;

public record GetAllEventsResponse(List<EventDto> Events);