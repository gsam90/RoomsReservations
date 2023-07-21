using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Data;
using RoomsReservations._1._Domain.Models;
using RoomsReservations.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository
    {
        public GuestRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
        }

        public async Task<List<Guest>?> GetWithDetailsAsync(Expression<Func<Guest, bool>>? filter)
        {
            return await _hotelDbContext.Set<Guest>()
                .Where(filter)
                .Include(x => x.Reservations)
                .ToListAsync();               
        }
    }
}
