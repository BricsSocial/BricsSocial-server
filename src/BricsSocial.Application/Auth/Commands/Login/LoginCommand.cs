using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Auth.Commands.Login
{
    public record class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // get UserInfo by email from identityService
            // sign in with password
            // generate token
        }
    }
}
