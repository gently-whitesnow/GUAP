namespace EBooks.Core.Entities.Reservation;

public class ReservationDbModel
{
    public uint Id { get; init; }
    public uint BookId { get; init; }
    public uint UserId { get; init; }
    public DateTimeOffset ReserveDate { get; init; }
}
