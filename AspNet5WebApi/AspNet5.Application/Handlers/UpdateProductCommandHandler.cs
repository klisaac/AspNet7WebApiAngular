using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet5.Core.Specifications;
using AspNet5.Core.Logging;
using AspNet5.Core.Repository;
using AspNet5.Application.Commands;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Common.Exceptions;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IMapper mapper, IAspNet5Logger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetSingleAsync(new ProductSpecification(request.ProductId));
            if (product == null)
                throw new BadRequestException($"Product with productId, {request.ProductId} does not exist.");

            product.Code = request.Code;
            product.Name = request.Name;
            product.Description = request.Description;
            product.UnitPrice = request.UnitPrice;
            product.CategoryId = request.CategoryId;
            product.LastModifiedBy = _currentUser.UserName;
            var productResponse = _mapper.Map<ProductResponse> (await _productRepository.UpdateByLoadingReferenceAsync(product, p => p.Category));
            _logger.LogInformation($"Updated product, {JsonSerializer.Serialize(productResponse)}.");

            return productResponse;
        }
    }
}