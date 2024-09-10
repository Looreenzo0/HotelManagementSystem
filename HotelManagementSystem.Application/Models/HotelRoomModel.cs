

using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.Models
{
    public class HotelRoomModel : BaseModel
    {
        public string? Name { get; set; }
        public string? RoomType { get; set; }
        public decimal? RoomRate { get; set; }
        public int? Availability { get; set; }
        public string? Status { get; set; }
    }
}
