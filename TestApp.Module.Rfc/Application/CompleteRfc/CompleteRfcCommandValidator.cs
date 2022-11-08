using FluentValidation;

namespace TestApp.Module.Rfc.Application.CompleteRfc
{
    internal class CompleteRfcCommandValidator : AbstractValidator<CompleteRfcCommand>
    {
        public CompleteRfcCommandValidator()
        {
            RuleFor(x => x.RfcId).NotEmpty().WithMessage("Rfc Id cannot be empty");
        }
    }
}