using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Exceptions;
using AspNet7.Application.Common.Identity;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IAspNet7Logger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, ICurrentUser currentUser, IAspNet7Logger<DeleteProductCommandHandler> logger)
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
            _logger.Information($"Deleted product with productId, {request.ProductId}.");
            return result != null;
        }
    }
}
