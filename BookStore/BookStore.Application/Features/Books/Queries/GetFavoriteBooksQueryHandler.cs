using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public class GetFavoriteBooksQueryHandler : IRequestHandler<GetFavoriteBooksQuery, IEnumerable<Book>>
    {
        private IUnitOfWork _unitOfWork;

        public GetFavoriteBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> Handle(GetFavoriteBooksQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetFavoriteBooksAsync(request.authorId);
        }
    }
}
