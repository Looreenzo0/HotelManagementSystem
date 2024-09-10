
using HotelManagementSystem.Application.Interfaces;

namespace HotelManagementSystem.Infrastructure.Services
{
    public class TimeProviderService : ITimeProviderService
    {
        private readonly TimeZoneInfo _defaultTimeZone;

        public TimeProviderService(Context.AppDbContext _context)
        {
            _defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
        }

        public DateTime GetCurrentTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _defaultTimeZone);
        }
    }
}
