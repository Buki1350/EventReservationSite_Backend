using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

internal sealed class ShoppingListNotFoundException(Guid id)
    : CustomException($"Shopping list with id {id} was not found.");