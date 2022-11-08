using FluentValidation;

namespace TestApp.Module.Rfc.Application.WithdrawRfc
{
    internal class WithdrawRfcCommandValidator : AbstractValidator<WithdrawRfcCommand>
    {
        public WithdrawRfcCommandValidator()
        {
            RuleFor(x => x.RfcId).NotEmpty().WithMessage("Rfc Id cannot be empty");
        }
    }
}