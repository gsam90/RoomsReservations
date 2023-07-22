using Application.Services.Implementations.Interfaces;
using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;
using RoomsReservations.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            try
            {
                var result = await _unitOfWork.Reservation.InsertAsync(reservation);
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating a reservation: {ex.Message}");
            }
        }

        public async Task<Reservation> DeleteAsync(Guid id)
        {
            try
            {
                var reservationToBeDeleted = await _unitOfWork.Reservation.GetByIdAsync(id);

                if (reservationToBeDeleted == null)
                {
                    return null;
                }

                _unitOfWork.Reservation.Delete(id);
                await _unitOfWork.Save();

                return reservationToBeDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the reservation: {ex.Message}");
            }
        }

        public async Task<List<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>> filter)
        {
            try
            {
                var result = await _unitOfWork.Reservation.GetAllAsync(filter);
                if (result == null)
                {
                    throw new Exception("No active reservation found!");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.Reservation.GetByIdAsync(id);
                if (result == null)
                {
                    throw new Exception("No active reservation found!");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation, Guid id)
        {
            try
            {
                var reservationToBeUpdated = await _unitOfWork.Reservation.GetByIdAsync(id);
                if (reservationToBeUpdated == null)
                {
                    throw new Exception($"Reservation with Id '{id}' not found");
                }
                var updatedReservation = await _unitOfWork.Reservation.Update(reservation);
                await _unitOfWork.Save();
                return updatedReservation;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while updating the reservation: {ex.Message}");
            }
        }
    }
}
