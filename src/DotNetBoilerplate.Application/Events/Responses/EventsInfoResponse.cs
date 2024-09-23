using DotNetBoilerplate.Application.Events.DTOs;
using DotNetBoilerplate.Core.Events;

namespace DotNetBoilerplate.Application.Events.Responses;

public class EventsInfoResponse
{
    public EventsInfoResponse(List<EventDto> events)
    {
        Events = events;
    }

    public List<EventDto> Events { get; set; }
}