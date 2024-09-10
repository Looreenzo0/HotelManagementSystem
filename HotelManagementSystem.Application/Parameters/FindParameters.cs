
namespace HotelManagementSystem.Application.Parameters
{
    public class FindParameters
    {
        public int Id { get; set; }
        public string? Find { get; set; }
        public string? Status { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Take { get; set; }
    }
}
