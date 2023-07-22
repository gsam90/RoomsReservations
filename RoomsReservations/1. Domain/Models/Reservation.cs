using RoomsReservations._1._Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RoomsReservations._1._Domain.Models;

public class Reservation : IDeletable
{
    
    public Guid Id { get; set; }

    
    public Guest? Guest { get; set; }

    
    public DateTime CheckInDate { get; set; }

    
    public DateTime CheckOutDate { get; set;}

    
    public string ReservationName { get; set;}

    
    public Room? Room { get; set;}

    
    public Payment WayOfPayment { get; set;}

    
    public PaymentStatus PaymentStatus { get; set;}

    public bool IsDeleted { get; set; }

}

