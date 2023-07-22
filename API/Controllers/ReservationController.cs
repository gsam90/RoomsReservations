using Microsoft.AspNetCore.Mvc;
using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
       private IUnitOfWork _unitOfWork;

        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAll()
        {
            var result = await _unitOfWork.Reservation.GetAllAsync();
            if (result == null)
            {
                Console.WriteLine("No reservations yet");
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetById(Guid id)
        {
            try
            {
                var result = await _unitOfWork.Reservation.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        private async Task<ActionResult<Reservation>> GetByIdWithDetails(Guid id)
        {
            var result = await _unitOfWork.Reservation.GetWithDetailsAsync(x => x.Id == id);
            return Ok(result.FirstOrDefault());
        }


        [HttpPost]
        public async Task<ActionResult<Reservation>> Create([FromBody] Reservation reservation)
        {
            var addedReservation = await _unitOfWork.Reservation.InsertAsync(reservation);
            return addedReservation;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> Delete(Guid id)
        {
            var reservation = await _unitOfWork.Reservation.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound(); // If the reservation with the given id doesn't exist
            }

            await _unitOfWork.Reservation.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> Update([FromBody] Reservation reservation)
        {
            var updatedApplication = await _unitOfWork.Reservation.Update(reservation);
            return Ok(updatedApplication);
        }
    }
}