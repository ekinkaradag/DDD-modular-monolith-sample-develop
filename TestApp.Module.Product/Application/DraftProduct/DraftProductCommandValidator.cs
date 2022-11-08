using FluentValidation;

namespace TestApp.Module.Product.Application.DraftProduct
{
    internal class DraftProductCommandValidator : AbstractValidator<DraftProductCommand>
    {
        public DraftProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id cannot be empty");
        }
    }
}