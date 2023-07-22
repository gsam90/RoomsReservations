namespace RoomsReservations._1._Domain.Interfaces
{
    public interface IDeletable : IEntity
    {
        bool IsDeleted { get; set; }
    }
}
