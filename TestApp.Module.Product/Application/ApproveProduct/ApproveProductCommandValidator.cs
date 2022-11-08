using FluentValidation;

namespace TestApp.Module.Product.Application.ApproveProduct
{
    internal class ApproveProductCommandValidator : AbstractValidator<ApproveProductCommand>
    {
        public ApproveProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id cannot be empty");
        }
    }
}