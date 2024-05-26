using FluentValidation;
using Agenda.Infrastructure.Models;

namespace Agenda.Infrastructure.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(contact => contact.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("O campo telefone(Phone) deve ser um número de telefone válido.");

            RuleFor(contact => contact.Email)
                .EmailAddress().WithMessage("O campo Email deve ser um endereço de email válido.");
        }
    }
}

