
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.Models
{
    public class ReservationModel : BaseModel
    {
        public string? RefNum { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public int Pax { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int RoomId { get; set; }
        public bool? IsWalkIn { get; set; }

        public string? RoomType { get; set; }
        public Decimal? RoomRate { get; set; }

        public int NumberOfNights
        {
            get
            {
                if (CheckIn.HasValue && CheckOut.HasValue)
                {
                    return (CheckOut.Value.Date - CheckIn.Value.Date).Days;
                }
                return 0; // Default to 0 if CheckIn or CheckOut is null
            }
        }
    }
}
