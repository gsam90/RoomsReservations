using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        
    }
}
