using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReservationDTO
    {
        public Guid Id { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string ReservationName { get; set; }
        public Guid? RoomId { get; set; } 
    }
}
