using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AspNet5.Application.Commands;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Common.Exceptions;
using AspNet5.Core.Specifications;
using AspNet5.Core.Logging;
using AspNet5.Core.Repository;

namespace AspNet5.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IAspNet5Logger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IAspNet5Logger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetSingleAsync(new ProductSpecification(request.ProductId));
            if (product == null)
                throw new BadRequestException($"Product with productId, {request.ProductId} does not exist.");

            product.LastModifiedBy = _currentUser.UserName;
            product.IsDeleted = true;
            var result = await _productRepository.UpdateAsync(product);
            _logger.LogInformation($"Deleted product with productId, {request.ProductId}.");
            return result != null;
        }
    }
}
