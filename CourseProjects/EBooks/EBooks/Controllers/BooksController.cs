using EBooks.BO.Services;
using Flow;
using Microsoft.AspNetCore.Mvc;

namespace EBooks.Controllers;

[Route("v1/books")]
public class BooksController : Controller
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet("{id}")]
    public IActionResult GetBookByIdAsync(uint id)
    {
        // todo user
        return _booksService.GetBookById(id, 1).AsActionResult();
    }
}