using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.UpdateCompany
{
    public sealed class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();
            RuleFor(v => v.Name)
                .Length(Company.Invariants.NameMinLength, Company.Invariants.NameMaxLength)
                .When(v => v.Name != null);
            RuleFor(v => v.Description)
                .Length(Company.Invariants.DescriptionMinLength, Company.Invariants.DescriptionMaxLength)
                .When(v => v.Description != null);
            RuleFor(v => v.Logo)
                .Length(Company.Invariants.LogoMinLength, Company.Invariants.LogoMaxLength)
                .When(v => v.Logo != null);
        }
    }
}
