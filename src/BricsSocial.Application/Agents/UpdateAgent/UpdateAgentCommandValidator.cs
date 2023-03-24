using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.UpdateAgent
{
    public sealed class UpdateAgentCommandValidator : AbstractValidator<UpdateAgentCommand>
    {
        public UpdateAgentCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotNull();
            RuleFor(v => v.FirstName)
                .Length(Agent.Invariants.FirstNameMinLength, Agent.Invariants.FirstNameMaxLength)
                .When(v => v.FirstName != null);
            RuleFor(v => v.LastName)
                .Length(Agent.Invariants.LastNameMinLength, Agent.Invariants.LastNameMaxLength)
                .When(v => v.LastName != null);
            RuleFor(v => v.Position)
                .Length(Agent.Invariants.PositionMinLength, Agent.Invariants.PositionMaxLength)
                .When(v => v.Position != null);
            RuleFor(v => v.Photo)
                .Length(Agent.Invariants.PhotoMinLength, Agent.Invariants.PhotoMaxLength)
                .When(v => v.Photo != null);
        }
    }
}
