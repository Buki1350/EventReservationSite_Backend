using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Shared.Abstractions.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public BusinessRuleValidationException(IBusinessRule rule) : base(rule.Message)
    {
        BrokenRule = rule;
    }

    public IBusinessRule BrokenRule { get; }
}