namespace Medhub_Backend.Application.Abstractions.Service;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}