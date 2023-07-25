using Application.DTOs;
using Application.Services.Implementations.Interfaces;
using AutoMapper;
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
    public class GuestService : IGuestService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GuestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guest> CreateAsync(Guest guest)
        {
            try
            {
                var result = await _unitOfWork.Guest.InsertAsync(guest);
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating a guest: {ex.Message}");
            }
        }

        public async Task<Guest> DeleteAsync(Guid id)
        {
            try
            {
                var guestToBeDeleted = await _unitOfWork.Guest.GetByIdAsync(id);

                if (guestToBeDeleted == null)
                {
                   return null;
                }

                await _unitOfWork.Guest.Delete(id);
                await _unitOfWork.Save();

                return guestToBeDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the guest: {ex.Message}");
            }
        }

        public async Task<List<GuestDTO>> GetAllAsync(Expression<Func<Guest, bool>> filter)
        {
            try
            {
                var guests = await _unitOfWork.Guest.GetAllAsync(filter);
                var guestDTOs = _mapper.Map<List<GuestDTO>>(guests);
                if (guestDTOs == null || guestDTOs.Count == 0)
                {
                    throw new Exception("No active guests found!");
                }
                return guestDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guest> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.Guest.GetByIdAsync(id);
                if (result == null)
                {
                    throw new Exception("No active guest found!");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guest> UpdateAsync(Guest guest, Guid id)
        {
            try
            {
                var guestToBeUpdated = await _unitOfWork.Guest.GetByIdAsync(id);
                if (guestToBeUpdated == null)
                {
                    throw new Exception($"Guest with Id '{id}' not found");
                }
                var updatedGuest = await _unitOfWork.Guest.Update(guest);
                await _unitOfWork.Save();
                return updatedGuest;
            }
             catch (Exception ex)
            {
                throw new Exception($"An error occured while updating the guest: {ex.Message}");
            }
        }
    }
}
