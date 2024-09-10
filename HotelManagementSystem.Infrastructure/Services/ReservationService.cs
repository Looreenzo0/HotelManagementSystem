

using AutoMapper;
using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Application.Models;
using HotelManagementSystem.Application.Parameters;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelManagementSystem.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITimeProviderService _timeProvider;

        public ReservationService(AppDbContext context, IMapper mapper, ITimeProviderService timeProvider)
        {
            _context = context;
            _mapper = mapper;
            _timeProvider = timeProvider;
        }

        private Reservation MapToEntity(ReservationModel model)
        {
            return _mapper.Map<ReservationModel, Reservation>(model);
        }

        public async Task<int> AddAsync(ReservationModel reserve)
        {
            DateTime currentTime = _timeProvider.GetCurrentTime();
            reserve.CreatedAt = currentTime;
            var reservation = MapToEntity(reserve);

            await _context.Reservations.AddAsync(reservation);
            //  await _context.SaveChangesAsync();
            return reservation.Id;
        }

        public async Task UpdateAsync(int id, ReservationModel reserve)
        {
            DateTime currentTime = _timeProvider.GetCurrentTime();

            reserve.UpdatedAt = currentTime;
            var updatedReservation = MapToEntity(reserve);

            var existingReservation = await _context.Reservations.FindAsync(id);
            if (existingReservation == null)
                throw new NullReferenceException($"Reservation with ID {id} not found");

            _mapper.Map(updatedReservation, existingReservation);
            existingReservation.UpdatedAt = updatedReservation.UpdatedAt;

            _context.Reservations.Update(existingReservation);
            //   await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                throw new NullReferenceException($"Reservation with ID {id} not found");

            _context.Reservations.Remove(reservation);
            //    await _context.SaveChangesAsync();
        }

        public async Task<ReservationModel> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return null;

            return _mapper.Map<ReservationModel>(reservation);
        }

        public IQueryable<ReservationModel> GetAllReservationsByParams(FindParameters find)
        {
            var query = from o in _context.Reservations
                        join o1 in _context.HotelRooms on o.RoomId equals o1.Id into o1Default
                        from o1 in o1Default.DefaultIfEmpty()
                        where (o.RefNum.Contains(find.Find) || o.Name.Contains(find.Find) || o.CreatedBy.Contains(find.Find))
                        && o.Status.Contains(find.Status)
                        && o.CreatedAt >= find.DateFrom && o.CreatedAt <= find.DateTo
                        select new ReservationModel
                        {
                            Id = o.Id,
                            RefNum = o.RefNum,
                            Name = o.Name,
                            Address = o.Address,
                            ContactNumber = o.ContactNumber,
                            Pax = o.Pax,
                            RoomType = o1.RoomType,
                            RoomRate = o1.RoomRate,
                            CheckIn = o.CheckIn,
                            CheckOut = o.CheckOut,
                            RoomId = o.RoomId,
                            Status = o.Status,
                            CreatedBy = o.CreatedBy,
                            CreatedAt = o.CreatedAt,
                            UpdatedAt = o.UpdatedAt
                        };

            return query.AsNoTracking().Take(find.Take);
        }
        

          public async Task<ReservationModel> GetReservationDataAsync(string refNum)
        {

            var dataEntity = await _context.Reservations.FirstOrDefaultAsync(c => c.RefNum == refNum);

            if (dataEntity == null)
            {
                return null;
            }

            var roomDetail = await _context.HotelRooms.FirstOrDefaultAsync(c => c.Id == dataEntity.RoomId);

            var reserveData = new ReservationModel
            {
                Id = dataEntity.Id,
                RefNum = dataEntity.RefNum,
                Name = dataEntity.Name,
                Address = dataEntity.Address,
                ContactNumber = dataEntity.ContactNumber,
                Pax = dataEntity.Pax,
                CheckIn = dataEntity.CheckIn,
                CheckOut = dataEntity.CheckOut,
                RoomId = dataEntity.RoomId,
                Status = dataEntity.Status,
                RoomType = roomDetail?.RoomType,
                RoomRate = roomDetail?.RoomRate,
 
            };

            return reserveData;

        }

    }
}
