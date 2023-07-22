using Application.DTOs;
using RoomsReservations._1._Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.Interfaces
{
    public interface IGuestService
    {
        Task<Guest> CreateAsync (Guest guest);
        Task<Guest> UpdateAsync (Guest guest, Guid id);
        Task<Guest> DeleteAsync (Guid id);
        Task<List<GuestDTO>> GetAllAsync (
            Expression<Func<Guest, bool>> filter);

        Task<Guest> GetByIdAsync (Guid id);

    }
}
