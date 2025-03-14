using FluentValidation;
using Domain.Entities;
using Domain.DTOs;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("El correo es obligatorio.")
           .EmailAddress().WithMessage("El correo debe tener un formato válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.");
    }
}
