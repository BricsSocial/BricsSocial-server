using BricsSocial.Application.Files.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.UpdateCompanyLogo
{
    public sealed class UpdateCompanyLogoCommandValidator : AbstractValidator<UpdateCompanyLogoCommand>
    {
        public UpdateCompanyLogoCommandValidator()
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
