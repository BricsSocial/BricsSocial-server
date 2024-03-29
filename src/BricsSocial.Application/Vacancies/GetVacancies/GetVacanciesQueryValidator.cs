﻿using BricsSocial.Application.SkillTags.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.GetVacancies
{
    public sealed class GetVacanciesQueryValidator : AbstractValidator<GetVacanciesQuery>
    {
        public GetVacanciesQueryValidator()
        {
            RuleFor(v => v.Status)
                .IsInEnum()
                .When(v => v.Status != null);
            RuleFor(v => v.SkillTags)
                .Must(SkillTagsUtils.BeListOfTags)
                .WithMessage(v => SkillTagsUtils.InvalidTagsMessage())
                .When(v => v.SkillTags != null);
        }
    }
}
