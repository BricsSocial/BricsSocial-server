using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.DeleteVacancy
{
    public sealed class DeleteVacancyCommandValidator : AbstractValidator<DeleteVacancyCommand>
    {
        public DeleteVacancyCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}
