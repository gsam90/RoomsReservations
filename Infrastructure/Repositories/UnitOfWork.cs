using Microsoft.EntityFrameworkCore;
using RoomsReservations;
using RoomsReservations._1._Domain.Data;
using RoomsReservations._1._Domain.Interfaces;
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
    public class UnitOfWork : IUnitOfWork
    {
        private HotelDbContext _hotelDbContext;

        public IReservationRepository Reservation { get; private set; }
        public IGuestRepository Guest { get; private set; }
        public IRoomRepository Room { get; private set; }


        public UnitOfWork(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
            Reservation = new ReservationRepository(_hotelDbContext);
            Guest = new GuestRepository(_hotelDbContext);
            Room = new RoomRepository(_hotelDbContext);
        }

        public async Task<TEntity> GetByIdWithDetails<TEntity>
            (
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity,
                object>>[] includePorperties
            ) 
            where TEntity : class

        {
            IQueryable<TEntity> baseQuery = _hotelDbContext.Set<TEntity>();
            baseQuery = baseQuery.Where(filter);
            foreach ( var entity in includePorperties)
            {
                baseQuery = baseQuery.Include(entity);
            }
            var result = baseQuery.FirstAsync();
            return await result;
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}
