using Microsoft.EntityFrameworkCore;
using RoomsReservations;
using RoomsReservations._1._Domain.Data;
using RoomsReservations._1._Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
        }

        public async Task<List<Room>?> GetWithDetailsAsync(Expression<Func<Room, bool>> filter)
        {
            return await _hotelDbContext.Set<Room>().ToListAsync();
        }
    }
}
