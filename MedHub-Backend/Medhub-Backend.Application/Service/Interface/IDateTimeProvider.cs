namespace Medhub_Backend.Application.Service.Interface;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}