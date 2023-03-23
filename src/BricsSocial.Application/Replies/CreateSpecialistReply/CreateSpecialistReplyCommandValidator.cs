using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.CreateSpecialistReply
{
    public sealed class CreateSpecialistReplyCommandValidator : AbstractValidator<CreateSpecialistReplyCommand>
    {
        public CreateSpecialistReplyCommandValidator()
        {
            RuleFor(v => v.SpecialistId)
                .NotNull();
            RuleFor(v => v.VacancyId)
                .NotNull();
        }
    }
}
