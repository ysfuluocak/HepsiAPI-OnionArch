using FluentValidation;

namespace HepsiAPI.Application.Features.Auths.Commands.Register
{
    public class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandRequestValidator()
        {
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(6);

            RuleFor(u => u.ConfirmPassword).NotEmpty();
            RuleFor(u => u.ConfirmPassword).MinimumLength(6);

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).MaximumLength(60);
            RuleFor(u => u.Email).EmailAddress();


            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).MinimumLength(3);
            RuleFor(u => u.FirstName).MaximumLength(50);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).MinimumLength(3);
            RuleFor(u => u.LastName).MaximumLength(50);

            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Username).MinimumLength(3);
            RuleFor(u => u.Username).MaximumLength(50);


        }
    }
}
