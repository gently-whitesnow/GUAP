namespace EBooks.Core.Entities.Reservation;

public class ReservationDbModel : DbModel
{
    public uint BookId { get; init; }
    public uint UserId { get; init; }
}
