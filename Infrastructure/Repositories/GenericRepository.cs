using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Data;
using RoomsReservations._1._Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected HotelDbContext _hotelDbContext;

        public GenericRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<bool>? Delete(TEntity entity)
        {
            try
            {
                _hotelDbContext.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _hotelDbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                List<TEntity> entities = new();
                if (filter != null)
                {
                    return entities = await _hotelDbContext.Set<TEntity>().Where(filter).ToListAsync();
                }
                else
                {
                    entities = await _hotelDbContext.Set<TEntity>().ToListAsync();
                }
                return await _hotelDbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity> GetByIdAsync(Guid Id)
        {
            try
            {
                var result = await _hotelDbContext.Set<TEntity>().FindAsync(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                var result = await _hotelDbContext.Set<TEntity>().FindAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {

                var result = _hotelDbContext.Set<TEntity>().Update(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
