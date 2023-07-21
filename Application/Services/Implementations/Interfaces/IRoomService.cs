using RoomsReservations._1._Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.Interfaces
{
    public interface IRoomService
    {
        Task<Room> CreateAsync(Room room);
        Task<Room> UpdateAsync(Room room, Guid id);
        Task<Room> DeleteAsync(Guid id);
        Task<List<Room>> GetAllAsync(
            Expression<Func<Room, bool>> filter);

        Task<Room> GetByIdAsync(Guid id);
    }
}
