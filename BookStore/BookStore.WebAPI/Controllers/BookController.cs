using BookStore.Application.Features.Books.Commands;
using BookStore.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _mediator.Send(new GetBookQuery());
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
        {
            var bookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBooks), new { id = bookId }, bookId);
        }
    }
}
