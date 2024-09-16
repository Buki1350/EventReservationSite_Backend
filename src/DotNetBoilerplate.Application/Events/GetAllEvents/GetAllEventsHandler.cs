using DotNetBoilerplate.Application.Events.DTOs;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Events.GetAllEvents;

public class GetAllEventsHandler : IQueryHandler<GetAllEventsQuery, EventsInfoResponse>
{
    private readonly IEventRepository _eventRepository;

    public GetAllEventsHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<EventsInfoResponse> HandleAsync(GetAllEventsQuery query)
    {
        var events = await _eventRepository.GetAllAsync();
        
        //LINQ
        var eventsDto = events.Select(@event => new EventDto(@event)).ToList();
        var eventsResponse = new EventsInfoResponse(eventsDto);
        
        return eventsResponse;
    }
}