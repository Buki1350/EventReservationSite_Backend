using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain;

public abstract class DomainService
{
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new BusinessRuleValidationException(rule);
    }
}