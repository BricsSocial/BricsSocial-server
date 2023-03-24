using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.GetSpecialist
{
    public sealed class GetSpecialistQueryValidator : AbstractValidator<GetSpecialistQuery>
    {
        public GetSpecialistQueryValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();
        }
    }
}
