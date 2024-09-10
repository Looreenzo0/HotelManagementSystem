
using HotelManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Domain.Entities
{
    public class HotelRoom : Base
    {
        public string? Name { get; set; }
        public string? RoomType { get; set; }
        public decimal? RoomRate { get; set; }
        public string? Status { get; set; }
        public int? Availability { get; set; }


        [NotMapped]
        public RoomStatus RoomStatusEnum
        {
            get => Enum.Parse<RoomStatus>(Status);
            set => Status = value.ToString();
        }
    }
}
