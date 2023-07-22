using RoomsReservations._1._Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsReservations._1._Domain.Models;

public class Reservation : IDeletable
{
    
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(Guest))]
    public Guid GuestId { get; set; }

    [Required]
    [ForeignKey(nameof(Room))]
    public Guid RoomId { get; set; }


    public DateTime CheckInDate { get; set; }

    
    public DateTime CheckOutDate { get; set;}

    
    public string ReservationName { get; set;}
    
    public Payment WayOfPayment { get; set;}

    
    public PaymentStatus PaymentStatus { get; set;}


    public bool IsDeleted { get; set; }

}

