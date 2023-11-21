namespace EBooks.Core.Entities.User;

public class UsersReservationView
{
    public UserReservationView[] Users { get; init; }
    public int Count { get; init; }
}