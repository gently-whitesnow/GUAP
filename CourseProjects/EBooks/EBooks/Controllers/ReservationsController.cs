using EBooks.Authorization;
using EBooks.BO.Services;
using EBooks.Core;
using EBooks.Core.Entities.Book;
using Flow;
using Microsoft.AspNetCore.Mvc;

namespace EBooks.Controllers;

[Route("v1/reservations")]
[FakeAuthorizationRequired]
[ValidateModelState]
public class ReservationsController : Controller
{
    private readonly ReservationsService _reservationsService;

    public ReservationsController(ReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }


    [HttpPost("{bookId}")]
    public IActionResult GetBookById(uint bookId)
    {
        return _reservationsService.Reserve(bookId, HttpContext.GetUser().Id).AsActionResult();
    }

    [HttpDelete("{bookId}")]
    public IActionResult GetBooksSummary(uint bookId)
    {
        return _reservationsService.UnReserve(bookId, HttpContext.GetUser().Id).AsActionResult();
    }
}