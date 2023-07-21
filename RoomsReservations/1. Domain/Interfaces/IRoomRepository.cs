using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RoomsReservations
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<List<Room>?> GetWithDetailsAsync(Expression<Func<Room, bool>> filter);
    }
}
