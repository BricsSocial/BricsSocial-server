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
            RuleFor(v => v.ShortBio)
                .Length(Specialist.Invariants.ShortBioMinLength, Specialist.Invariants.ShortBioMaxLength)
                .When(v => v.ShortBio != null);
            RuleFor(v => v.LongBio)
                .Length(Specialist.Invariants.LongBioMinLength, Specialist.Invariants.LongBioMaxLength)
                .When(v => v.LongBio != null);
            RuleFor(v => v.Photo)
                .Length(Specialist.Invariants.PhotoMinLength, Specialist.Invariants.PhotoMaxLength)
                .When(v => v.Photo != null);
        }
    }
}
