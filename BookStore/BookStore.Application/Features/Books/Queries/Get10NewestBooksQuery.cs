using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public record Get10NewestBooksQuery : IRequest<IEnumerable<Book>>
    {
    }
}
