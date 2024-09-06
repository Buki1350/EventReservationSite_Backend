namespace DotNetBoilerplate.Shared.Abstractions.Time;

public interface IClock
{
    DateTime Now();
    DateTimeOffset DateTimeOffsetNow();
}