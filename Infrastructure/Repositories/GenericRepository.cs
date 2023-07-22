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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected HotelDbContext _hotelDbContext;

        public GenericRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<bool> Delete(Guid entityId)
        {
            try
            {
                var entity = await _hotelDbContext.Set<TEntity>().FindAsync(entityId);
                if (entity != null)
                {
                    // Check if the entity supports soft delete (has the IsDeleted property)
                    if (entity is IDeletable deletableEntity)
                    {
                        deletableEntity.IsDeleted = true;
                        await _hotelDbContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        // If the entity does not support soft delete, perform a hard delete
                        _hotelDbContext.Set<TEntity>().Remove(entity);
                        await _hotelDbContext.SaveChangesAsync();
                        return true;
                    }
                }
                return false; // Return false if the entity with the given ID was not found.
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

        //public async Task<List<TEntity>> GetAllAsync()
        //{
        //    try
        //    {
        //        return await _hotelDbContext.Set<TEntity>().Where(e => !(e is IDeletable deletable) || !deletable.IsDeleted).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                // Check if the TEntity implements IDeletable
                var isDeletableEntity = typeof(TEntity).GetInterfaces().Contains(typeof(IDeletable));

                var query = _hotelDbContext.Set<TEntity>().AsQueryable();

                // Apply the soft delete filter if TEntity implements IDeletable
                if (isDeletableEntity)
                {
                    query = query.Where(e => !(e as IDeletable).IsDeleted);
                }

                // Apply additional filter if provided
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                // Check if the TEntity implements IDeletable
                var isDeletableEntity = typeof(TEntity).GetInterfaces().Contains(typeof(IDeletable));

                var query = _hotelDbContext.Set<TEntity>().Where(e => e.Id == id);

                // Apply the soft delete filter if TEntity implements IDeletable
                if (isDeletableEntity)
                {
                    query = query.Where(e => !(e as IDeletable).IsDeleted);
                }

                return await query.FirstOrDefaultAsync();
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
                var result = await _hotelDbContext.Set<TEntity>().AddAsync(entity);
                await _hotelDbContext.SaveChangesAsync();
                return result.Entity;
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
                await _hotelDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                // Check if the TEntity implements IDeletable
                var isDeletableEntity = typeof(TEntity).GetInterfaces().Contains(typeof(IDeletable));

                var query = _hotelDbContext.Set<TEntity>().Where(e => e.Id == id);

                // Apply the soft delete filter if TEntity implements IDeletable
                if (isDeletableEntity)
                {
                    query = query.Where(e => !(e as IDeletable).IsDeleted);
                }

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
