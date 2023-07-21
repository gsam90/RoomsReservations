using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace RoomsReservations.RepoInterfaces
{
    public interface IGuestRepository : IGenericRepository<Guest>
    {
        Task<List<Guest>?> GetWithDetailsAsync(Expression<Func<Guest, bool>> filter);
    }
}
