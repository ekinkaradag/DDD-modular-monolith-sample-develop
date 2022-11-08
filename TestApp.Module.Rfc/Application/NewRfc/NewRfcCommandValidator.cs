using FluentValidation;
using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.Module.Rfc.Application.NewRfc
{
    internal class NewRfcCommandValidator : CommandValidatorBase<NewRfcCommand>
    {
        public NewRfcCommandValidator()
        {
            RuleFor(x => x.Key).NotEmpty().WithMessage("RFC Key cannot be empty");
            RuleFor(x => x.Title).NotEmpty().WithMessage("RFC Title cannot be empty");
        }
    }
}