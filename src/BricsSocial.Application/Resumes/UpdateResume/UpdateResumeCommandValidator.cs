using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.UpdateResume
{
    public sealed class UpdateResumeStatusCommandValidator : AbstractValidator<UpdateResumeCommand>
    {
        public UpdateResumeStatusCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
            RuleFor(v => v.Skills)
                .Length(Resume.Invariants.SkillsMinLength, Resume.Invariants.SkillsMaxLength)
                .When(v => v.Skills != null);
            RuleFor(v => v.Experience)
                .Length(Resume.Invariants.ExperienceMinLength, Resume.Invariants.ExperienceMaxLength)
                .When(v => v.Experience != null);
        }
    }
}
