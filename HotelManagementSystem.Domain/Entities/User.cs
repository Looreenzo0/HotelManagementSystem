
namespace HotelManagementSystem.Domain.Entities
{
    public class User : Base
    {
        public int ContactId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Role (e.g., Admin, User)

        public virtual Contact Contact { get; set; }
    }
}
