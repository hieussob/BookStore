using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Features.Books.Queries
{
    public class GetCategoryBookQueryHandler : IRequestHandler<GetCategoryBookQuery, IEnumerable<Book>>
    {
        private IUnitOfWork _unitOfWork;

        public GetCategoryBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Book>> Handle(GetCategoryBookQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookRepository.GetBooksByCategoryAsync(request.id, request.count);
        }
    }
}
