using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.CommonExceptions;

public class InvalidEntityStringException() : CustomException($"The string value cannot be null or empty.");