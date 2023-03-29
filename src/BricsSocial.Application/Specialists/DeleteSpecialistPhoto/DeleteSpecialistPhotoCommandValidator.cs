using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.DeleteSpecialistPhoto
{
    public sealed class DeleteSpecialistPhotoCommandValidator : AbstractValidator<DeleteSpecialistPhotoCommand>
    {
        public DeleteSpecialistPhotoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}
