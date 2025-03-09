using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Commands
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Price = request.Price
            };
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangeAsync();

            return book.Id;
        }
    }
}
