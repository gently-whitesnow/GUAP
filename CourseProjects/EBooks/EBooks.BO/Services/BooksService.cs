using EBooks.Core.Entities.Book;
using EBooks.Core.Entities.User;
using EBooks.Core.Mappers;
using EBooks.DA.Repositories;
using Flow;

namespace EBooks.BO.Services;

public class BooksService
{
    private readonly BooksRepository _booksRepository;
    private readonly ReservationsRepository _reservationsRepository;
    private readonly UsersRepository _usersRepository;

    public BooksService(BooksRepository booksRepository,
        ReservationsRepository reservationsRepository,
        UsersRepository usersRepository)
    {
        _booksRepository = booksRepository;
        _reservationsRepository = reservationsRepository;
        _usersRepository = usersRepository;
    }
    
    public Operation<BookView> GetBookById(uint id, uint? requesterUserId = null)
    {
        var bookDbModelOperation = _booksRepository.GetById(id);
        if (bookDbModelOperation.IsNotSuccess)
            return bookDbModelOperation.FlowError<BookView>();

        var reservations = _reservationsRepository.GetAll()
            .Where(r => r.BookId == id).ToArray();

        var users = _usersRepository.GetAll();
        
        var userReservationViews = new List<UserReservationView>(reservations.Length);
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
            
            if(userReservationViews.Count == reservations.Length)
                break;
        }

        return bookDbModelOperation.Value.ToBookView(new UsersReservationView
        {
            Users = userReservationViews.ToArray(),
            Count = userReservationViews.Count
        });
    }
    
    public BooksSearchView GetBooksSummary(BookSummaryDto bookSummaryDto)
    {
        var books = _booksRepository.GetAll()
            .Where(b=>b.Title.Contains(bookSummaryDto.SearchQuery) 
                      || b.Author.Contains(bookSummaryDto.SearchQuery))
            .Skip(bookSummaryDto.Skip)
            .Take(bookSummaryDto.Take)
            .Select(b=>new BookSummaryView
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Count = b.Count
            }).ToArray();

        SetBooksCountAvailability(books);

        return new BooksSearchView
        {
            Books = books,
            TotalCount = books.Length
        };
    }
    
    public BookDbModel UpsertBook(BookUpsertDto bookUpsertDto)
    {
        var book = new BookDbModel
        {
            Id = bookUpsertDto.Id,
            Title = bookUpsertDto.Title,
            Description = bookUpsertDto.Description,
            Author = bookUpsertDto.Author,
            Count = bookUpsertDto.Count
        };
        
        return _booksRepository.Upsert(book);
    }
    
    public void DeleteBook(uint bookId)
    {
        _booksRepository.Delete(bookId);
    }
    
    private void SetBooksCountAvailability(BookSummaryView[] books)
    {
        var reservations = _reservationsRepository.GetAll()
            .GroupBy(b => b.BookId)
            .ToDictionary(pair => pair.Key, groups=>groups.Count());
        
        foreach (var book in books)
        {
            if (reservations.TryGetValue(book.Id, out var reservationsCount))
                book.AvailableCount = book.Count - reservationsCount;
            else
                book.AvailableCount = book.Count;
        }
    }
}