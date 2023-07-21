using RoomsReservations.RepoInterfaces;
using System.Linq.Expressions;

namespace RoomsReservations._1._Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IGuestRepository Guest { get; }
        public IReservationRepository Reservation { get; }
        public IRoomRepository Room { get; }

        public Task<TEntity> GetByIdWithDetails<TEntity>(Expression<Func<TEntity, bool>> filter, params
            Expression<Func<TEntity, object>>[] includePorperties) where TEntity : class;
        public Task<bool> Save();

    }
}
