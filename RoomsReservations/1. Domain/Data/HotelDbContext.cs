using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Models;
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

   
}


