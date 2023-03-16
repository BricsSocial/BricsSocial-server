using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.UpdateVacancyStatus
{
    public sealed class UpdateVacancyStatusCommandValidator : AbstractValidator<UpdateVacancyStatusCommand>
    {
        public UpdateVacancyStatusCommandValidator()
        {
            RuleFor(v => v.VacancyId)
                .NotNull();
            RuleFor(v => v.VacancyStatus)
                .IsInEnum();
        }
    }
}
