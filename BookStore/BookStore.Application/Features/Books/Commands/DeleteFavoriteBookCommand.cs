using MediatR;

namespace BookStore.Application.Features.Books.Commands
{
    public record DeleteFavoriteBookCommand(Guid bookId, Guid authorId) : IRequest<Guid>
    {
    }
}
