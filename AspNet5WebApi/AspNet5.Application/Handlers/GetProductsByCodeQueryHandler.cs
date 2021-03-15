using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using AspNet5.Core.Specifications;
using AspNet5.Core.Repository;
using AspNet5.Application.Queries;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class GetProductsByCodeQueryHandler : IRequestHandler<GetProductsByCodeQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCodeQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByCodeQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetAsync(new ProductSpecification(request.ProductCode)));
        }
    }
}
