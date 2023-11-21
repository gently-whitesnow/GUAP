namespace EBooks.Core.Entities.User;

public class UserReservationView
{
    public uint ReservationId { get; init; }
    public DateTimeOffset AddDate { get; init; }
    public string Email { get; init; }
    public string FullName { get; init; }
    public bool IsOwner { get; init; }
}