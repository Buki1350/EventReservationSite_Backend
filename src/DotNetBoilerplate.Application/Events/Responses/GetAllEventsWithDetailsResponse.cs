using DotNetBoilerplate.Application.Events.DTOs;

namespace DotNetBoilerplate.Application.Events.Responses;

public record GetAllEventsWithDetailsResponse(List<EventDetailsDto> Events);