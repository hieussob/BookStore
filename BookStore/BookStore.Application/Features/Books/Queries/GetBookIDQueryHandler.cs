using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public class GetBookIdQueryHandler : IRequestHandler<GetBookIdQuery, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(GetBookIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetByIdAsync(request.id) ?? new Book();
        }
    }
}
