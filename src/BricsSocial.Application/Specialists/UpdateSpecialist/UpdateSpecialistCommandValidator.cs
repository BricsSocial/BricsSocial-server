using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.UpdateSpecialist
{
    public sealed class UpdateSpecialistCommandValidator : AbstractValidator<UpdateSpecialistCommand>
    {
        public UpdateSpecialistCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();

            RuleFor(v => v.FirstName)
                .Length(Specialist.Invariants.FirstNameMinLength, Specialist.Invariants.FirstNameMaxLength)
                .When(v => v.FirstName != null);
            RuleFor(v => v.LastName)
                .Length(Specialist.Invariants.LastNameMinLength, Specialist.Invariants.LastNameMaxLength)
                .When(v => v.LastName != null);

            RuleFor(v => v.Bio)
                .Length(Specialist.Invariants.BioMinLength, Specialist.Invariants.BioMaxLength)
                .When(v => v.Bio != null);
            RuleFor(v => v.Skills)
                .Length(Specialist.Invariants.SkillsMinLength, Specialist.Invariants.SkillsMaxLength)
                .When(v => v.Skills != null);
            RuleFor(v => v.Experience)
                .Length(Specialist.Invariants.ExperienceMinLength, Specialist.Invariants.ExperienceMaxLength)
                .When(v => v.Experience != null);

            RuleFor(v => v.Photo)
                .Length(Specialist.Invariants.PhotoMinLength, Specialist.Invariants.PhotoMaxLength)
                .When(v => v.Photo != null);
        }
    }
}
