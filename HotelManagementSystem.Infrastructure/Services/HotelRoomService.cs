
using AutoMapper;
using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Application.Models;
using HotelManagementSystem.Application.Parameters;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Infrastructure.Services
{

    public class HotelRoomService : IHotelRoomService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITimeProviderService _timeProvider;

        public HotelRoomService(AppDbContext context, IMapper mapper, ITimeProviderService timeProvider)
        {
            _context = context;
            _mapper = mapper;
            _timeProvider = timeProvider;
        }

        private HotelRoom MapToEntity(HotelRoomModel model)
        {
            return _mapper.Map<HotelRoomModel, HotelRoom>(model);
        }

        public async Task<int> AddAsync(HotelRoomModel room)
        {
            DateTime currentTime = _timeProvider.GetCurrentTime();
            room.CreatedAt = currentTime;
            var hotelRoom = MapToEntity(room);

            await _context.HotelRooms.AddAsync(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom.Id;
        }

        public async Task UpdateAsync(int id, HotelRoomModel room)
        {
            DateTime currentTime = _timeProvider.GetCurrentTime();

            room.UpdatedAt = currentTime;
            var updatedRoom = MapToEntity(room);

            var existingRoom = await _context.HotelRooms.FindAsync(id);
            if (existingRoom == null)
                throw new NullReferenceException($"Room with ID {id} not found");

            _mapper.Map(updatedRoom, existingRoom);
            existingRoom.UpdatedAt = updatedRoom.UpdatedAt;

            _context.HotelRooms.Update(existingRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.HotelRooms.FindAsync(id);
            if (room == null)
                throw new NullReferenceException($"Room with ID {id} not found");

            _context.HotelRooms.Remove(room);
        }

        public IQueryable<HotelRoomModel> GetAllHotelRoomByParams(FindParameters find)
        {
            var query = from o in _context.HotelRooms

                        where (o.Name.Contains(find.Find))
                        && o.Status.Contains(find.Status)
                        && o.CreatedAt >= find.DateFrom && o.CreatedAt <= find.DateTo
                        select new HotelRoomModel
                        {
                            Id = o.Id,
                            Name = o.Name,
                            RoomType = o.RoomType,
                            RoomRate = o.RoomRate,
                            Status = o.Status,
                            CreatedBy = o.CreatedBy,
                            CreatedAt = o.CreatedAt,
                            UpdatedAt = o.UpdatedAt
                        };

            return query.AsNoTracking().Take(find.Take);
        }

        public IQueryable<HotelRoomModel> SearchHotelRoomByParams(FindParameters find)
        {
            var query = from o in _context.HotelRooms

                        where (o.Name.Contains(find.Find) || o.RoomType.Contains(find.Find))
                        select new HotelRoomModel
                        {
                            Id = o.Id,
                            Name = o.Name,
                            RoomType = o.RoomType,
                            RoomRate = o.RoomRate,
                            Status = o.Status,
                        };

            return query.AsNoTracking().Take(find.Take);
        }

        public async Task<HotelRoomModel> GetHotelRoomByIdAsync(int id)
        {
            var room = await _context.HotelRooms
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                return null;

            return _mapper.Map<HotelRoomModel>(room);
        }




    }
}
