using FluentValidation;
using ShoppingAdmin.Models;
using ShoppingAdmin.Validators.Extensions;

namespace ShoppingAdmin.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email).NotEmpty().WithMessage("Email adresi zorunludur.")
                                 .EmailAddress().WithMessage("Lütfen geçerli bir mail adresi yazınız");

            RuleFor(l => l.Password).Password();

        }
    }
}
