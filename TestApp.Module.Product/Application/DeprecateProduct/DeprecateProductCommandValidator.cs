using FluentValidation;

namespace TestApp.Module.Product.Application.DeprecateProduct
{
    internal class DeprecateProductCommandValidator : AbstractValidator<DeprecateProductCommand>
    {
        public DeprecateProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id cannot be empty");
        }
    }
}