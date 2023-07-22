using RoomsReservations._1._Domain.Models;
using System.Linq.Expressions;

namespace RoomsReservations._1._Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// This method retrieves entities from the database, applying an optional filter expression delegate,
        /// and returns a list of entities that following the filter parameters.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns >
        /// A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
        /// </returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(Guid Id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool>? Delete(Guid Id);
    }
}
