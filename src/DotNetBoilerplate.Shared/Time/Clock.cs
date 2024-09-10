using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Shared.Time;

public class Clock : IClock
{
    public DateTime Now()
    {
        return DateTime.UtcNow;
    }

    public DateTimeOffset DateTimeOffsetNow()
    {
        return DateTimeOffset.Now;
    }
}