using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace RoomsReservations._1._Domain.Data;

public class HotelDbContext : DbContext
{
    
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Guest> Guests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connection = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=RoomsManagement;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True;"; // Replace this with the actual connection string for your database.
            optionsBuilder.UseSqlServer(connection, b => b.MigrationsAssembly("RoomsReservations"));
        }
    }
}


