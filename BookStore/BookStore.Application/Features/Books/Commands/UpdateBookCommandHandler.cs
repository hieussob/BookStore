using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(request.id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.Title = request.book.Title;
            book.Price = request.book.Price;
            book.Description = request.book.Description;

            _unitOfWork.BookRepository.Update(book);

            await _unitOfWork.SaveChangeAsync();
            return book.Id;

        }
    }
}
