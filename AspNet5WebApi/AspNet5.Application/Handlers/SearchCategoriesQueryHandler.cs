using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNet5.Core.Pagination;
using AspNet5.Core.Repository;
using AspNet5.Application.Queries;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class SearchCategoriesQueryHandler : IRequestHandler<SearchCategoriesQuery, IPagedList<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SearchCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IPagedList<CategoryResponse>> Handle(SearchCategoriesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IPagedList<CategoryResponse>>(await _categoryRepository.SearchCategoriesAsync(request.Args));
        }
    }
}
