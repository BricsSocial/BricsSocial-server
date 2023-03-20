using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.DeleteAgent
{
    public sealed class DeleteAgentCommandValidator : AbstractValidator<DeleteAgentCommand>
    {
        public DeleteAgentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}
