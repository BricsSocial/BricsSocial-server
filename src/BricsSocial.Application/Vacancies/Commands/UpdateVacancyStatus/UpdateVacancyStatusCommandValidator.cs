using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using BricsSocial.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Commands.UpdateVacancyStatus
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
