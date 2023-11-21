using EBooks.Core.Entities;
using EBooks.Core.Entities.Reservation;
using EBooks.Core.Entities.User;
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
    
    public Operation<UsersReservationView> Reserve(uint bookId, uint requesterUserId)
    {
        var bookDbModelOperation = _booksRepository.GetById(bookId);
        if (bookDbModelOperation.IsNotSuccess)
            return bookDbModelOperation.FlowError<UsersReservationView>();

        var reservations = _reservationsRepository.GetAll()
            .Where(r => r.BookId == bookId).ToList();

        if(bookDbModelOperation.Value.Count <= reservations.Count)
            return Operation<UsersReservationView>.Failed(Errors.NotAvailableBooksError);
        
        if (reservations.Any(reservation => reservation.UserId == requesterUserId))
        {
            return Operation<UsersReservationView>.Failed(Errors.AlreadyReserveByUserError);
        }
        
        var reservationDbModel = new ReservationDbModel
        {
            BookId = bookId,
            UserId = requesterUserId
        };
        reservations.Add(_reservationsRepository.Upsert(reservationDbModel));

        var users = _usersRepository.GetAll();
        
        var userReservationViews = new List<UserReservationView>(reservations.Count);
        foreach (var user in users)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.UserId != user.Id)
                    continue;
                
                userReservationViews.Add(new UserReservationView
                {
                    ReservationId = reservation.Id,
                    AddDate = reservation.AddDate,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsOwner = reservation.UserId == requesterUserId
                });
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
    
    public Operation UnReserve(uint reservationId, uint requesterUserId)
    {
        var reservationOperation = _reservationsRepository.GetById(reservationId);
        if(reservationOperation.IsNotSuccess)
            return reservationOperation.FlowError();

        if (reservationOperation.Value.UserId != requesterUserId)
            return Operation.Failed(Errors.NotOwnerOfReservationError);

        return _reservationsRepository.Delete(reservationId);
    }
}