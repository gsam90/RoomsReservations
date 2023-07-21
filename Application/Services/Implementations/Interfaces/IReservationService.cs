using RoomsReservations._1._Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(Reservation reservation);
        Task<Reservation> UpdateAsync(Reservation reservation, Guid id);
        Task<Reservation> DeleteAsync(Guid id);
        Task<List<Reservation>> GetAllAsync(
            Expression<Func<Reservation, bool>> filter);

        Task<Reservation> GetByIdAsync(Guid id);
    }
}
