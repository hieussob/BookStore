using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public record GetFavoriteBooksQuery(Guid authorId) : IRequest<IEnumerable<Book>>
    {
    }
}
