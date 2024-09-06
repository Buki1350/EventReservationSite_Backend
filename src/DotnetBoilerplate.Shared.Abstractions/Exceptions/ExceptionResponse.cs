using System.Net;

namespace DotNetBoilerplate.Shared.Abstractions.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);