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
    
    public Result<BookView> GetBookById(uint id, uint requesterUserId)
    {
        var bookDbModelOperation = _booksRepository.GetById(id);
        if (bookDbModelOperation.HasError)
            return bookDbModelOperation.Error;

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
                
                userReservationViews.Add(user.ToUserReservationView(reservation, requesterUserId));
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
            .Where(b=>string.IsNullOrEmpty(bookSummaryDto.SearchQuery) 
                      || b.Title.Contains(bookSummaryDto.SearchQuery) 
                      || b.Author.Contains(bookSummaryDto.SearchQuery))
            .OrderByDescending(b=>b.AddDate)
            .Skip(bookSummaryDto.Skip)
            .Take(bookSummaryDto.Take)
            .Select(b=>b.ToBookSummaryView()).ToArray();

        SetBooksCountAvailability(books);

        if (bookSummaryDto.IsAvailable)
            books = books.Where(b => b.AvailableCount != 0).ToArray();

        return new BooksSearchView
        {
            Books = books,
            TotalCount = books.Length
        };
    }
    
    public List<YourBookView> GetYourBooks(uint requesterUserId)
    {
        var reservations = _reservationsRepository.GetAll()
            .Where(r => r.UserId == requesterUserId).ToArray();

        var books = _booksRepository.GetAll();
        
        var userBooks = new List<YourBookView>(reservations.Length);
        foreach (var book in books)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.BookId != book.Id)
                    continue;
                
                userBooks.Add(new YourBookView
                {
                    Title = book.Title,
                    Author = book.Author,
                    ReservationId = reservation.Id
                });
                break;
            }
            
            if(userBooks.Count == reservations.Length)
                break;
        }

        return userBooks;
    }
    
    public BookDbModel UpsertBook(BookUpsertDto bookUpsertDto)
    {
        return _booksRepository.Upsert(bookUpsertDto.ToBookDbModel());
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