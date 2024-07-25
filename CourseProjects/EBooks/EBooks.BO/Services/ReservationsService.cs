using EBooks.Core.Entities;
using EBooks.Core.Entities.Reservation;
using EBooks.Core.Entities.User;
using EBooks.Core.Mappers;
using EBooks.DA.Repositories;
using Flow;

namespace EBooks.BO.Services;

public class ReservationsService
{
    private readonly BooksRepository _booksRepository;
    private readonly ReservationsRepository _reservationsRepository;    
    private readonly UsersRepository _usersRepository;

    public ReservationsService(BooksRepository booksRepository,
        ReservationsRepository reservationsRepository,
        UsersRepository usersRepository)
    {
        _booksRepository = booksRepository;
        _reservationsRepository = reservationsRepository;
        _usersRepository = usersRepository;
    }
    
    public Result<UsersReservationView> Reserve(uint bookId, uint requesterUserId)
    {
        var bookDbModelOperation = _booksRepository.GetById(bookId);
        if (bookDbModelOperation.HasError)
            return bookDbModelOperation.Error;

        var reservations = _reservationsRepository.GetAll()
            .Where(r => r.BookId == bookId).ToList();

        if(bookDbModelOperation.Value.Count <= reservations.Count)
            return Errors.NotAvailableBooksError;
        
        if (reservations.Any(reservation => reservation.UserId == requesterUserId))
        {
            return Errors.AlreadyReserveByUserError;
        }
        
        var reservationDbModel = new ReservationDbModel
        {
            BookId = bookId,
            UserId = requesterUserId
        };
        reservations.Add(_reservationsRepository.Insert(reservationDbModel));

        var users = _usersRepository.GetAll();
        
        var userReservationViews = new List<UserReservationView>(reservations.Count);
        foreach (var user in users)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.UserId != user.Id)
                    continue;
                
                userReservationViews.Add(user.ToUserReservationView(reservation, requesterUserId));
                break;
            }
            
            if(userReservationViews.Count == reservations.Count)
                break;
        }
        
        return new UsersReservationView
        {
            Users = userReservationViews.ToArray(),
            Count = userReservationViews.Count
        };
    }
    
    public Result UnReserve(uint reservationId, uint requesterUserId)
    {
        var reservationOperation = _reservationsRepository.GetById(reservationId);
        if(reservationOperation.HasError)
            return reservationOperation.Error;

        if (reservationOperation.Value.UserId != requesterUserId)
            return Errors.NotOwnerOfReservationError;

        return _reservationsRepository.Delete(reservationId);
    }
}