using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public class Get10NewestCommandHandler : IRequestHandler<Get10NewestBooksCommand, IEnumerable<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Get10NewestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> Handle(Get10NewestBooksCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetNewestBooksAsync(count: 10);
        }
    }
}
