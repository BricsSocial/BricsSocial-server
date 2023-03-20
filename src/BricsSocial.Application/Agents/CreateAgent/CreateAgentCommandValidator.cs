using BricsSocial.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.RegisterAgent
{
    public sealed class CreateAgentCommandValidator : AbstractValidator<CreateAgentCommand>
    {
        public CreateAgentCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty();
            RuleFor(e => e.Password)
                .NotEmpty();
            RuleFor(v => v.FirstName)
                .NotEmpty()
                .Length(Agent.Invariants.FirstNameMinLength, Agent.Invariants.FirstNameMaxLength);
            RuleFor(v => v.LastName)
                .NotEmpty()
                .Length(Agent.Invariants.LastNameMinLength, Agent.Invariants.LastNameMaxLength);
            RuleFor(v => v.CompanyId)
                .NotNull();
        }
    }
}
