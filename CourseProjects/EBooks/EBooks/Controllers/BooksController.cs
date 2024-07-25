using System.ComponentModel.DataAnnotations;
using EBooks.Authorization;
using EBooks.BO.Services;
using EBooks.Core;
using EBooks.Core.Entities.Book;
using Flow;
using Microsoft.AspNetCore.Mvc;

namespace EBooks.Controllers;

[Route("v1/books")]
[FakeAuthorizationRequired]
[ValidateModelState]
public class BooksController : Controller
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet("{id}")]
    public Task<IResult> GetBookById(uint id)
    {
        var user = HttpContext.GetUser();
        return _booksService.GetBookById(id, user.Id).AsResultAsync();
    }
    
    [HttpPost("search")]
    public Task<BooksSearchView> GetBooksSummary([FromBody][Required] BookSummaryDto bookSummaryDto)
    {
        return _booksService.GetBooksSummary(bookSummaryDto);
    }
    
    [HttpGet("your")]
    public Task<List<YourBookView>> GetYourBooks()
    {
        return _booksService.GetYourBooks(HttpContext.GetUser().Id);
    }
    
    [HttpPost]
    public Task<IResult> UpsertBookAsync([FromForm][Required] BookUpsertDto bookUpsertDto)
    {
        return _booksService.UpsertBookAsync(bookUpsertDto).AsResultAsync();
    }
    
    [HttpDelete("{id}")]
    public Task DeleteBook(uint id)
    {
        return _booksService.DeleteBook(id);
    }
}