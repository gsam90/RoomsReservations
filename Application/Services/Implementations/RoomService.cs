using Application.Services.Implementations.Interfaces;
using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Room> CreateAsync(Room room)
        {
            try
            {
                var result = await _unitOfWork.Room.InsertAsync(room);
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating a room: {ex.Message}");
            }
        }

        public async Task<Room> DeleteAsync(Guid id)
        {
            try
            {
                var roomToBeDeleted = await _unitOfWork.Room.GetByIdAsync(id);

                if (roomToBeDeleted == null)
                {
                    return null;
                }

                _unitOfWork.Room.Delete(roomToBeDeleted);
                await _unitOfWork.Save();

                return roomToBeDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the room: {ex.Message}");
            }
        }

        public async Task<List<Room>> GetAllAsync(Expression<Func<Room, bool>> filter)
        {
            try
            {
                var result = await _unitOfWork.Room.GetAllAsync(filter);
                if (result == null)
                {
                    throw new Exception("No active room found!");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Room> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.Room.GetByIdAsync(id);
                if (result == null)
                {
                    throw new Exception("No active room found!");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Room> UpdateAsync(Room room, Guid id)
        {
            try
            {
                var roomToBeUpdated = await _unitOfWork.Room.GetByIdAsync(id);
                if (roomToBeUpdated == null)
                {
                    throw new Exception($"Room with Id '{id}' not found");
                }
                var updatedRoom = await _unitOfWork.Room.Update(room);
                await _unitOfWork.Save();
                return updatedRoom;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while updating the room: {ex.Message}");
            }
        }
    }
}
