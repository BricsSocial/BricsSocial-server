using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.CreateCompany
{
    public sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .Length(Company.Invariants.NameMinLength, Company.Invariants.NameMaxLength);
            RuleFor(v => v.Description)
                .NotEmpty()
                .Length(Company.Invariants.DescriptionMinLength, Company.Invariants.DescriptionMaxLength);
            RuleFor(v => v.Logo)
                .Length(Company.Invariants.LogoMinLength, Company.Invariants.LogoMaxLength)
                .When(v => v.Logo != null);
            RuleFor(v => v.CountryId)
                .NotNull();
        }
    }
}
