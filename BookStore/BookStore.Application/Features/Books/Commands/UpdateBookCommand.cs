using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Features.Books.Commands
{
    public record UpdateBookCommand(Guid id, Book book) : IRequest<Guid>
    {
    }
}
