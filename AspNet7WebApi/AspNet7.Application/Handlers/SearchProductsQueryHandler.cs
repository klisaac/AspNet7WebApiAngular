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
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IPagedList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public SearchProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IPagedList<ProductResponse>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var productPagedList = await _productRepository.SearchProductsAsync(request.Args);
            return _mapper.Map<IPagedList<ProductResponse>>(productPagedList);
        }
    }
}
