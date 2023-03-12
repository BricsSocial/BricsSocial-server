using BricsSocial.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Commands.CreateVacancy
{
    public class CreateVacancyCommandValidator : AbstractValidator<CreateVacancyCommand>
    {
        private const int NameMaxLength = 100;
        private const int RequirementsMaxLength = 1500;
        private const int OfferingsMaxLength = 1500;

        public CreateVacancyCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(v => $"{nameof(v.Name)} is required.")
                .MaximumLength(NameMaxLength).WithMessage(v => $"{nameof(v.Name)} must not exceed {NameMaxLength} characters.");
            RuleFor(e => e.Requirements)
                .NotEmpty().WithMessage(v => $"{nameof(v.Requirements)} are required")
                .MaximumLength(RequirementsMaxLength).WithMessage(v => $"{nameof(v.Requirements)} must not exceed {RequirementsMaxLength} characters.");
            RuleFor(e => e.Offerings)
                .NotEmpty().WithMessage(v => $"{nameof(v.Offerings)} are required")
                .MaximumLength(OfferingsMaxLength).WithMessage(v => $"{nameof(v.Offerings)} must not exceed {OfferingsMaxLength} characters.");
        }
    }
}
