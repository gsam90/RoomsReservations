using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Data;
using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;
using RoomsReservations.RepoInterfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(HotelDbContext hotelDbContext) : base (hotelDbContext) 
        { 
        }
        
        public async Task<List<Reservation>?> GetWithDetailsAsync(Expression<Func<Reservation, bool>>? filter)
        {
            try
            {
                return await _hotelDbContext.Reservations
                    .Where(filter)
                    .Include(x => x.Room)
                    .ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}