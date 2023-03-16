using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.CreateVacancy
{
    public class CreateVacancyCommandValidator : AbstractValidator<CreateVacancyCommand>
    {

        public CreateVacancyCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(Vacancy.Invariants.NameMaxLength);
            RuleFor(e => e.Requirements)
                .NotEmpty()
                .MaximumLength(Vacancy.Invariants.RequirementsMaxLength);
            RuleFor(e => e.Offerings)
                .NotEmpty()
                .MaximumLength(Vacancy.Invariants.OfferingsMaxLength);
        }
    }
}
