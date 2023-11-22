using EBooks.Core.Entities.Book;
using EBooks.Core.Entities.Reservation;
using EBooks.Core.Entities.User;

namespace EBooks.Core.Mappers;

public static class UserMapper
{
    public static UserReservationView ToUserReservationView(this UserDbModel user, ReservationDbModel reservation, uint requesterUserId)
    {
        return new UserReservationView
        {
            ReservationId = reservation.Id,
            AddDate = reservation.AddDate,
            Email = user.Email,
            FullName = user.FullName,
            IsOwner = reservation.UserId == requesterUserId
        };
    }
}