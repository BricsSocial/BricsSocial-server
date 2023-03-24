using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.UpdateVacancyReply
{
    public sealed class UpdateVacancyReplyCommandValidator : AbstractValidator<UpdateVacancyReplyCommand>
    {
        public UpdateVacancyReplyCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
            RuleFor(v => v.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
