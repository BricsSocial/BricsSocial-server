using BricsSocial.Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.CreateSpecialist
{
    public sealed class CreateSpecialistCommandValidator : AbstractValidator<CreateSpecialistCommand>
    {
        public CreateSpecialistCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty();
            RuleFor(e => e.Password)
                .NotEmpty();
            RuleFor(v => v.FirstName)
                .NotEmpty()
                .Length(Specialist.Invariants.FirstNameMinLength, Specialist.Invariants.FirstNameMaxLength);
            RuleFor(v => v.LastName)
                .NotEmpty()
                .Length(Specialist.Invariants.LastNameMinLength, Specialist.Invariants.LastNameMaxLength);
            RuleFor(v => v.CountryId)
                .NotNull();
        }
    }
}
