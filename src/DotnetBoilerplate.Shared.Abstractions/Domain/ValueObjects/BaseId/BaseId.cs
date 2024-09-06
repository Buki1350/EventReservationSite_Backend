using DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.BaseId.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.BaseId;

public abstract class BaseId : IEquatable<BaseId>
{
    protected BaseId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidBaseIdException(value);

        Value = value;
    }

    public Guid Value { get; }

    public bool Equals(BaseId other)
    {
        return Value == other?.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        return obj is BaseId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(BaseId obj1, BaseId obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null)) return true;

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(BaseId x, BaseId y)
    {
        return !(x == y);
    }
}