using DemoApp.Application.Users.Commands.RegisterUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public LoginUserCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
