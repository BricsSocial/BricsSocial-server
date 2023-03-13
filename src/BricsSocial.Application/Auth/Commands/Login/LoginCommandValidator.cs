using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage(v => $"{nameof(v.Email)} is required.");
            RuleFor(e => e.Password)
                .NotEmpty().WithMessage(v => $"{nameof(v.Password)} is required.");
        }
    }
}
