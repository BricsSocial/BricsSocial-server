using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.GetReplies
{
    public sealed class GetVacancyRepliesQueryValidator : AbstractValidator<GetVacancyRepliesQuery>
    {
        public GetVacancyRepliesQueryValidator()
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
