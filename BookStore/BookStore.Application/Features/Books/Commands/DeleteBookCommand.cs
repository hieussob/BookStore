using MediatR;

namespace BookStore.Application.Features.Books.Commands
{
    public record DeleteBookCommand(Guid id) : IRequest<Guid>
    {
    }
}
