using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public class Get10NewestQueryHandler : IRequestHandler<Get10NewestBooksQuery, IEnumerable<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Get10NewestQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> Handle(Get10NewestBooksQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetNewestBooksAsync(count: 10);
        }
    }
}
