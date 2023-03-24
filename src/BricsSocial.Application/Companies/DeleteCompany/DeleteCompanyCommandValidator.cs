using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.DeleteCompany
{
    public sealed class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}
