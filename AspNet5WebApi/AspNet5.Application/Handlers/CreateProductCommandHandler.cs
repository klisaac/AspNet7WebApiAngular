using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet5.Application.Commands;
using AspNet5.Application.Common.Exceptions;
using AspNet5.Core.Entities;
using AspNet5.Core.Logging;
using AspNet5.Core.Specifications;
using AspNet5.Core.Repository;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IMapper mapper, IAspNet5Logger<CreateProductCommandHandler> logger)
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
            _logger.LogInformation($"Created product, {JsonSerializer.Serialize(productResponse)}.");

            return productResponse;
        }
    }
}