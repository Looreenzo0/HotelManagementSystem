
using HotelManagementSystem.Application.Models;
using HotelManagementSystem.Application.Parameters;

namespace HotelManagementSystem.Application.Interfaces
{
    public interface IHotelRoomService
    {
        Task<int> AddAsync(HotelRoomModel room);
        Task UpdateAsync(int id, HotelRoomModel room);
        Task DeleteAsync(int id);

        Task<HotelRoomModel> GetHotelRoomByIdAsync(int id);
        IQueryable<HotelRoomModel> GetAllHotelRoomByParams(FindParameters find); 
        IQueryable<HotelRoomModel> SearchHotelRoomByParams(FindParameters find);
    }
}
