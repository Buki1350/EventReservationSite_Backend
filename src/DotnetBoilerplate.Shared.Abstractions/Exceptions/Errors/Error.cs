namespace DotNetBoilerplate.Shared.Abstractions.Exceptions.Errors;

public record Error(string Code, string Reason, string? Property = null);

public record ErrorsResponse(params Error[] Errors);