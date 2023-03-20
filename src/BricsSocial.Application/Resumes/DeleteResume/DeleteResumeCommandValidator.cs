using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.DeleteResume
{
    public sealed class DeleteResumeCommandValidator : AbstractValidator<DeleteResumeCommand>
    {
        public DeleteResumeCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}
