using DotNetBoilerplate.Core.Events;

namespace DotNetBoilerplate.Application.Events.DTOs;

public record EventDto
{
    public EventDto(Event @event)
    {
        Id = @event.Id;
        Title = @event.Title;
    }

    public Guid Id { get; init; }
    public string Title { get; init; }
}