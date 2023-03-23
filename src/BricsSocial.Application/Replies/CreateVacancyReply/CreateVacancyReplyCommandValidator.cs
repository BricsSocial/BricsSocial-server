using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.CreateVacancyReply
{
    public sealed class CreateVacancyReplyCommandValidator : AbstractValidator<CreateVacancyReplyCommand>
    {
        public CreateVacancyReplyCommandValidator()
        {
            RuleFor(v => v.VacancyId)
                .NotNull();
        }
    }
}
