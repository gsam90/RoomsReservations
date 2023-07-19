using System.ComponentModel.DataAnnotations;

namespace RoomsReservations._1._Domain.Models;

public class Reservation
{
    
    public Guid Id { get; set; }

    [Required]
    public Guest Guest { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set;}

    [Required]
    public string ReservationName { get; set;}

    [Required]
    public Room Room { get; set;}

    [Required]
    public Payment WayOfPayment { get; set;}

    [Required]
    public PaymentStatus PaymentStatus { get; set;}

}

