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
    public class GetBooksFilterQueryHandler : IRequestHandler<GetBooksFilterQuery, IEnumerable<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBooksFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Book>> Handle(GetBooksFilterQuery request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.BookRepository.GetBooksFilterAsync(request.keyword, request.categoryId, request.rangeFrom, request.rangeTo, request.order, request.page);
            
            return books as IEnumerable<Book> ?? new List<Book>();

        }
    }
}
