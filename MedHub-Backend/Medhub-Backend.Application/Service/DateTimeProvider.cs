using Medhub_Backend.Application.Service.Interface;

namespace Medhub_Backend.Application.Service;

// this service is used instead of using UtcNow directly for better unit testing
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
}