
using HotelManagementSystem.Application.Models;
using HotelManagementSystem.Application.Parameters;

namespace HotelManagementSystem.Application.Interfaces
{
    public interface IReservationService
    {
        Task<int> AddAsync(ReservationModel reserve);
        Task UpdateAsync(int id, ReservationModel reserve);
        Task DeleteAsync(int id);

        Task<ReservationModel> GetReservationByIdAsync(int id);
        IQueryable<ReservationModel> GetAllReservationsByParams(FindParameters find);
        Task<ReservationModel> GetReservationDataAsync(string refNum);
    }
}
