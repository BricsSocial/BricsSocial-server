using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.GetCompany
{
    public sealed class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
    {
        public GetCompanyQueryValidator()
        {
            RuleFor(v => v.Id)
              .NotNull();
        }
    }
}
