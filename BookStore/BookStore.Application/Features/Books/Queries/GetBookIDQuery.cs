using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public record GetBookIdQuery(Guid id) : IRequest<Book>
    {
    }
}
