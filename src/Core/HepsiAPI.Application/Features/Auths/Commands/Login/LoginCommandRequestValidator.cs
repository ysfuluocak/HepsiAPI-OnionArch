using FluentValidation;

namespace HepsiAPI.Application.Features.Auths.Commands.Login
{
    public class LoginCommandRequestValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandRequestValidator()
        {
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(6);

            RuleFor(u => u.ConfirmPassword).NotEmpty();
            RuleFor(u => u.ConfirmPassword).MinimumLength(6);

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).MaximumLength(60);
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}
