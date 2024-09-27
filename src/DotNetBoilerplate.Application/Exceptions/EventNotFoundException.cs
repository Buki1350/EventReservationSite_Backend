using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

public class EventNotFoundException(Guid value)
    : CustomException($"Event with the given address: {value} was not found");