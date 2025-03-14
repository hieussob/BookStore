using BookStore.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Features.Books.Commands
{
    public class DeleteFavoriteBookCommandHandler : IRequestHandler<DeleteFavoriteBookCommand, Guid>
    {
        private IUnitOfWork _unitOfWork;

        public DeleteFavoriteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteFavoriteBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.DeleteFavoriteBooksAsync(request.bookId, request.authorId);
            return request.bookId;

        }
    }
}
