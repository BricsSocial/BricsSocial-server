using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.UpdateSpecialistReply
{
    public sealed class UpdateSpecialistReplyCommandValidator : AbstractValidator<UpdateSpecialistReplyCommand>
    {
        public UpdateSpecialistReplyCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
            RuleFor(v => v.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
