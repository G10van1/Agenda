using FluentValidation;
using Agenda.Infrastructure.Models;

namespace Agenda.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("O campo UserName é requerido.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("O campo Senha é requerido.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");

            RuleFor(user => user.Email)
                .EmailAddress().WithMessage("O campo Email deve ser um endereço de email válido.");
        }
    }
}

