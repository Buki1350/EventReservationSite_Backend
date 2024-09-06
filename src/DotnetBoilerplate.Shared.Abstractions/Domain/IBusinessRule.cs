namespace DotNetBoilerplate.Shared.Abstractions.Domain;

public interface IBusinessRule
{
    string Message { get; }
    bool IsBroken();
}