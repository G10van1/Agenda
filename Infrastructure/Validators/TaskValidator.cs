using FluentValidation;
using Agenda.Infrastructure.Models;

namespace Agenda.Infrastructure.Validators
{
    public class TaskValidator : AbstractValidator<Agenda.Infrastructure.Models.Task>
    {
        public TaskValidator()
        {
            RuleFor(task => task.Title)
                .NotEmpty().WithMessage("O campo Title é requerido.")
                .Length(1, 100).WithMessage("O campo Title deve estar entre 1 e 100 caracteres.");

            RuleFor(task => task.Description)
                .NotEmpty().WithMessage("O campo Description é requerido.");

            RuleFor(task => task.Date)
                .Must(date => date != DateTime.MinValue)
                .WithMessage("O campo Date não pode ser vazio ou valor default.");

            RuleFor(task => task.Status)
                .Must(status => ((int)status) >= 0 || ((int)status) <= 3)
                .WithMessage("O Status deve estar no intervalo entra 0 e 3.");
        }
    }
}
