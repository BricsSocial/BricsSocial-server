using BricsSocial.Application.SkillTags.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.GetSpecialists
{
    public sealed class GetSpecialistsQueryValidator : AbstractValidator<GetSpecialistsQuery>
    {
        public GetSpecialistsQueryValidator()
        {
            RuleFor(v => v.SkillTags)
                .Must(SkillTagsUtils.BeListOfTags)
                .WithMessage(v => SkillTagsUtils.InvalidTagsMessage())
                .When(v => v.SkillTags != null);
        }
    }
}
