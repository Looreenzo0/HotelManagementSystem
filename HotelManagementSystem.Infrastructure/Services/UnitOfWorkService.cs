
using AutoMapper;
using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Infrastructure.Context;

namespace HotelManagementSystem.Infrastructure.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext _context;
        private readonly IMapper? _mapper;
        public UnitOfWorkService(AppDbContext context, IMapper mapper)
        {
            _context = context;

            TimeProviderServices = new TimeProviderService(_context);
            Reservations = new ReservationService(_context, mapper, TimeProviderServices);
            HotelRooms = new HotelRoomService(_context, mapper, TimeProviderServices);

        }
        public ITimeProviderService TimeProviderServices { get; }
        public IReservationService Reservations { get; }
        public IHotelRoomService HotelRooms { get; }



        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
