using MediatR;

namespace BookStore.Application.Features.Books.Commands
{
    public record AddBookCommand(string Title, int Price) : IRequest<Guid>
    {

    }
}
