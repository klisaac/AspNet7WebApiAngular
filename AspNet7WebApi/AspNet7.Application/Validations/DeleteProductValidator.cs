using AspNet7.Application.Commands;
using FluentValidation;

namespace AspNet7.Application.Validations
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(request => request.ProductId).GreaterThan(0);
        }
    }
}
