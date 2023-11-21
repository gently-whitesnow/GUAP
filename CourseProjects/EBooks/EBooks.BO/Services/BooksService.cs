using EBooks.Core.Entities.Book;
using EBooks.DA.Repositories;
using Flow;

namespace EBooks.BO.Services;

public class BooksService
{
    private readonly BooksRepository _booksRepository;
    private readonly ReservationsRepository _reservationsRepository;

    public BooksService(BooksRepository booksRepository,
        ReservationsRepository reservationsRepository)
    {
        _booksRepository = booksRepository;
        _reservationsRepository = reservationsRepository;
    }
    
    public Operation<BookDbModel> GetBookById(uint id)
    {
        return _booksRepository.GetById(id);
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