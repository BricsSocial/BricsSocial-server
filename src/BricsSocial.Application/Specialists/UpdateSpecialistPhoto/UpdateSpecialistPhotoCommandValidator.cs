using BricsSocial.Application.Files.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.UpdateSpecialistPhoto
{
    public sealed class UpdateSpecialistPhotoCommandHandlerValidator : AbstractValidator<UpdateSpecialistPhotoCommand>
    {
        public UpdateSpecialistPhotoCommandHandlerValidator()
        {
            RuleFor(v => v.Id)
                .NotNull();

            RuleFor(v => v.File)
                .NotNull()
                .Must(FileUtils.BeNotEmptyFile!)
                .WithMessage(v => FileUtils.CannotByEmptyFileMessage());

            RuleFor(v => v.File)
                .Must(FileUtils.HaveValidContentType!)
                .WithMessage(v => FileUtils.InvalidContentTypeMessage(v.File.ContentType));
        }
    }
}
