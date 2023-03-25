using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Auth.Current
{

    [Authorize(Roles = UserRoles.All)]
    public record GetCurrentUserQuery : IRequest<CurrentUserDto>
    {
    }

    public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identityService;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUser, IIdentityService identityService)
        {
            _currentUser = currentUser;
            _identityService = identityService;
        }

        public async Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await _identityService.GetUserInfoAsync(_currentUser.UserId);

            if (userInfo == null)
                throw new Exception($"Current user not found ({_currentUser.UserId})");

            var dto = new CurrentUserDto
            {
                Email = userInfo.Email,
                Role = userInfo.Role
            };

            return dto;
        }
    }

}
