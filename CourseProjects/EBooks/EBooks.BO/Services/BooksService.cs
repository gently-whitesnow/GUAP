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
    
    public Operation<BookDbModel[]> GetBooksSummary(SummaryBookDto summaryBookDto)
    {
        var books = _booksRepository.GetAll()
            .Where(b=>b.Title.Contains(summaryBookDto.SearchQuery) 
                      || b.Author.Contains(summaryBookDto.SearchQuery))
            .Skip(summaryBookDto.Skip)
            .Take(summaryBookDto.Take);

        return books.ToArray();
    }
}