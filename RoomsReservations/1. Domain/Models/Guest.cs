using RoomsReservations._1._Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RoomsReservations._1._Domain.Models;

public class Guest : IDeletable
{
    
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required] 
    public string LastName { get;set; }

    
    public DateTime? BirthDate { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

    
    public Sex? Sex { get; set; }

    public List<Reservation>? Reservations { get; set; }

    public bool IsDeleted { get; set; }

    public Guest()
    {
        Reservations = new List<Reservation>();
    }


}

