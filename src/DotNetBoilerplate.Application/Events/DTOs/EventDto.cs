using DotNetBoilerplate.Core.Events;

namespace DotNetBoilerplate.Application.Events.DTOs;

public record EventDto(Guid Id, string Title)
{
    public EventDto(Event @event) : this(@event.Id, @event.Title) { }
}