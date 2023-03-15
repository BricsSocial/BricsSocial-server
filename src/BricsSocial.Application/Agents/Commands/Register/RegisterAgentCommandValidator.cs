using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.Commands.Register
{
    public sealed class RegisterAgentCommandValidator : AbstractValidator<RegisterAgentCommand>
    {
        public RegisterAgentCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .NotEmpty()
                .Length(AgentLimitations.FirstNameMinLength, AgentLimitations.FirstNameMaxLength);
            RuleFor(e => e.LastName)
                .NotEmpty()
                .Length(AgentLimitations.LastNameMinLength, AgentLimitations.LastNameMaxLength);
            RuleFor(v => v.Email)
                .NotEmpty();
            RuleFor(e => e.Password)
                .NotEmpty();
            RuleFor(e => e.Position)
                .NotEmpty()
                .Length(AgentLimitations.PositionMinLength, AgentLimitations.PositionMaxLength);
            RuleFor(e => e.CompanyId)
                .NotNull();
        }
    }
}
