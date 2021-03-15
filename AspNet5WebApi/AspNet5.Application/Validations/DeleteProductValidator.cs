using AspNet5.Application.Commands;
using FluentValidation;

namespace AspNet5.Application.Validations
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(request => request.ProductId).GreaterThan(0);
        }
    }
}
