using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Auth.Commands.Login
{
    public record class LoginCommand : IRequest<TokenResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userInfo = await _identityService.GetUserInfoByEmailAsync(request.Email);
            if (userInfo == null)
                throw new Exception($"User with email '{request.Email}' not existing!");

            var checkPassword = await _identityService.CheckPasswordAsync(request.Email, request.Password);

            if (!checkPassword)
                throw new Exception("Password check failed!");

            var token = _jwtProvider.Generate(userInfo);

            return new TokenResponse
            {
                Token = token
            };
        }
    }
}
