using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.GetVacancy
{
    public sealed class GetVacancyQueryValidator : AbstractValidator<GetVacancyQuery>
    {
        public GetVacancyQueryValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();
        }
    }
}
