using System.ComponentModel.DataAnnotations;

namespace RoomsReservations._1._Domain.Models;

public class Room
{
    
    public Guid Id { get; set; }

    [Required]
    public string RoomName { get; set; }

    [Required]
    public int RoomCapacity { get; set; }

    [Required]
    public int RoomPrice { get; set; }

    [Required]
    public string? RoomType { get; set; }

    [Required]
    public RoomStatus Status { get; set; }

}
