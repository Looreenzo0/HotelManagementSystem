
using HotelManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Domain.Entities
{
    public class Reservation : Base
    {
        public string? RefNum { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public int Pax { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int RoomId { get; set; }
        public string? Status { get; set; }
        public bool? IsWalkIn { get; set; }

        [NotMapped]
        public ReservationStatus ReservationStatusEnum
        {
            get => Enum.Parse<ReservationStatus>(Status);
            set => Status = value.ToString();
        }

    }
}
