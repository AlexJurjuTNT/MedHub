namespace Medhub_Backend.Business.Service.Interface;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}