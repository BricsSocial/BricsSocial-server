using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.GetSpecialistReplies
{
    public sealed class GetSpecialistRepliesQueryValidator : AbstractValidator<GetSpecialistRepliesQuery>
    {
        public GetSpecialistRepliesQueryValidator()
        {
            RuleFor(v => v.Status)
                .IsInEnum()
                .When(v => v.Status != null);
            RuleFor(v => v.Type)
                .IsInEnum()
                .When(v => v.Type != null);
        }
    }
}
