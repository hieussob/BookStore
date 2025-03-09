using BookStore.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Features.Books.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(request.id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            _unitOfWork.BookRepository.Delete(book);
            await _unitOfWork.SaveChangeAsync();

            return book.Id;
        }

    }
}
