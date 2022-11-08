using FluentValidation;
using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.Module.Product.Application.NewProduct
{
    internal class NewRfcCommandValidator : CommandValidatorBase<NewProductCommand>
    {
        public NewRfcCommandValidator()
        {
            RuleFor(x => x.Key).NotEmpty().WithMessage("Product Version cannot be empty");
            RuleFor(x => x.Version).NotEmpty().WithMessage("Product Version cannot be empty");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Product Title cannot be empty");
        }
    }
}