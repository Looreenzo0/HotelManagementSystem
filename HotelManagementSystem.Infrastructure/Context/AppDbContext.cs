
using HotelManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
