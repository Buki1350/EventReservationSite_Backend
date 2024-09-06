namespace DotNetBoilerplate.Shared.Abstractions.Domain;

public interface IBusinessRuleAsync
{
    string Message { get; }
    Task<bool> IsBroken();
}