using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;

namespace BookStore.Application.Features.Books.Queries
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, IEnumerable<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetAllAsync();
        }
    }
}
