using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNet7.Application.Queries;
using AspNet7.Application.Responses;
using AspNet7.Core.Pagination;
using AspNet7.Core.Repository;

namespace AspNet7.Application.Handlers
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
