using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.GetAgent
{
    public sealed class GetAgentQueryValidator : AbstractValidator<GetAgentQuery>
    {
        public GetAgentQueryValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();
        }
    }
}
