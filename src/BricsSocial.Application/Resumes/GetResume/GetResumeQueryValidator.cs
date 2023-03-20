using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.GetResume
{
    public sealed class GetResumeQueryValidator : AbstractValidator<GetResumeQuery>
    {
        public GetResumeQueryValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();
        }
    }
}
