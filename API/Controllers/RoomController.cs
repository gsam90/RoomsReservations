using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using RoomsReservations._1._Domain.Interfaces;
using RoomsReservations._1._Domain.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAll()
        {
            var result = await _unitOfWork.Room.GetAllAsync();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetById(Guid id)
        {
            var result = await _unitOfWork.Room.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Room>> GetByIdWithDetails(Guid id)
        {
            var result = await _unitOfWork.Room.GetWithDetailsAsync(x => x.Id == id);
            return Ok(result.FirstOrDefault());
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Create([FromBody] Room Room)
        {
            var addedRoom = await _unitOfWork.Room.InsertAsync(Room);
            return addedRoom;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> Delete(Guid id)
        {
            var room = await _unitOfWork.Room.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            // Soft delete the room if it's IDeletable (supports soft delete)
            if (room is IDeletable deletableRoom)
            {
                deletableRoom.IsDeleted = true;
                await _unitOfWork.Room.Update(room);
            }
            else
            {
                // Perform a hard delete if the room doesn't support soft delete
                await _unitOfWork.Room.Delete(id);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Room>> Update([FromBody] Room Room)
        {
            var updatedRoom = await _unitOfWork.Room.Update(Room);
            return Ok(updatedRoom);
        }
    }
}
