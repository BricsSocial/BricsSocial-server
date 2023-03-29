using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.DeleteAgentPhotoCommand
{
    public sealed class DeleteAgentPhotoCommandValidator : AbstractValidator<DeleteAgentPhotoCommand>
    {
        public DeleteAgentPhotoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}
