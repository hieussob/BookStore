using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public record GetCategoryBookQuery(Guid id, int count) : IRequest<IEnumerable<Book>>
    {
    }
}
