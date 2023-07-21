using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;
using System.Linq.Expressions;

namespace RoomsReservations.RepoInterfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<List<Reservation>?> GetWithDetailsAsync(Expression<Func<Reservation, bool>> filter);
    }
}
