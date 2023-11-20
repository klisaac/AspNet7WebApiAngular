using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Exceptions;
using AspNet7.Application.Common.Identity;
using AspNet7.Application.Responses;
using AspNet7.Core.Entities;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet7Logger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IMapper mapper, IAspNet7Logger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _productRepository.CountAsync(new ProductSpecification(request.Code)) > 0)
                throw new BadRequestException("Product code already exists.");
                
            var product = _mapper.Map<Product>(request);
            product.IsDeleted = false;
            product.CreatedBy = _currentUser.UserName;

            var productResponse = _mapper.Map<ProductResponse>(await _productRepository.AddByLoadingReferenceAsync(product, p => p.Category));
            _logger.Information($"Created product, {JsonSerializer.Serialize(productResponse)}.");

            return productResponse;
        }
    }
}