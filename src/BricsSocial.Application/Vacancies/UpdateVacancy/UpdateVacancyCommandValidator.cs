using BricsSocial.Application.SkillTags.Utils;
using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.UpdateVacancy
{
    public sealed class UpdateVacancyCommandValidator : AbstractValidator<UpdateVacancyCommand>
    {
        public UpdateVacancyCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
            RuleFor(v => v.Name)
                .Length(Vacancy.Invariants.NameMinLength, Vacancy.Invariants.NameMaxLength)
                .When(v => v.Name != null);
            RuleFor(v => v.Requirements)
                .Length(Vacancy.Invariants.RequirementsMinLength, Vacancy.Invariants.RequirementsMaxLength)
                .When(v => v.Requirements != null);
            RuleFor(v => v.Offerings)
                .Length(Vacancy.Invariants.OfferingsMinLength, Vacancy.Invariants.OfferingsMaxLength)
                .When(v => v.Offerings != null);
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
