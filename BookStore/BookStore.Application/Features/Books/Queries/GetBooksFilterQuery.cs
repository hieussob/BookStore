using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public record GetBooksFilterQuery(string keyword, Guid categoryId, int rangeFrom, int rangeTo, int order, int page) : IRequest<IEnumerable<Book>>
    {
    }
}
