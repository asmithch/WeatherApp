using FluentValidation;
using AuthenticationService.Application.DTOs;

namespace AuthenticationService.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}