using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Exceptions;
using AspNet7.Application.Common.Identity;
using AspNet7.Application.Responses;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet7Logger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IMapper mapper, IAspNet7Logger<UpdateProductCommandHandler> logger)
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
            _logger.Information($"Updated product, {JsonSerializer.Serialize(productResponse)}.");

            return productResponse;
        }
    }
}