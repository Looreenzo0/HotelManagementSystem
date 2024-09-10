
namespace HotelManagementSystem.Application.Interfaces
{
    public interface IUnitOfWorkService : IDisposable
    {
        ITimeProviderService TimeProviderServices { get; }
        IReservationService Reservations { get; }
        IHotelRoomService HotelRooms { get; }
        Task<int> CompleteAsync();
    }
}
