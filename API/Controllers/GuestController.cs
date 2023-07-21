using Microsoft.AspNetCore.Mvc;
using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public GuestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Guest>>> GetAll()
        {
            var result = await _unitOfWork.Guest.GetAllAsync();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetById(Guid id)
        {
            var result = await _unitOfWork.Guest.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Guest>> GetByIdWithDetails(Guid id)
        {
            var result = await _unitOfWork.Guest.GetWithDetailsAsync(x => x.Id == id);
            return Ok(result.FirstOrDefault());
        }

        [HttpPost]
        public async Task<ActionResult<Guest>> Create([FromBody] Guest guest)
        {
            var addedGuest = await _unitOfWork.Guest.InsertAsync(guest);
            return addedGuest;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guest>> Delete(Guest guest)
        {
            await _unitOfWork.Guest.Delete(guest);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guest>> Update([FromBody] Guest guest)
        {
            var updatedGuest = await _unitOfWork.Guest.Update(guest);
            return Ok(updatedGuest);
        }
    }
}
