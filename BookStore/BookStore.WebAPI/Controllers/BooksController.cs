using BookStore.Application.Features.Books.Commands;
using BookStore.Application.Features.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var res = await _mediator.Send(new GetBookIdQuery(id));

            return Ok(res);
        }
        [HttpGet("getAllSanPhamWithFilter/{keyword}/{categoryId}/{rangeFrom}/{rangeTo}/{order}/{page}")]
        public async Task<ActionResult<Book>> getAllSanPhamWithFilter(string keyword, Guid categoryId, int rangeFrom, int rangeTo, int order, int page)
        {
            var result = await _mediator.Send(new GetBooksFilterQuery(keyword, categoryId, rangeFrom, rangeTo, order, page));

            return Ok(result);
        }

    }
}
