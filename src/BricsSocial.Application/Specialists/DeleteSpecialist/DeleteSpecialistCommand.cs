using BricsSocial.Application.Agents.DeleteAgent;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.DeleteSpecialist
{
    [Authorize(Roles = UserRoles.AdministratorAndSpecialist)]
    public record class DeleteSpecialistCommand : IRequest
    {
        public int? Id { get; init; }
    }

    public sealed class DeleteSpecialistCommandHandler : IRequestHandler<DeleteSpecialistCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;

        public DeleteSpecialistCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUser, IUserService userService)
        {
            _context = context;
            _identityService = identityService;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task Handle(DeleteSpecialistCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _context.Specialists
               .Where(c => c.Id == request.Id)
               .FirstOrDefaultAsync();

            if (specialist == null)
                throw new NotFoundException(nameof(specialist), request.Id);

            if (_currentUser.Role == UserRoles.Specialist)
                _userService.CheckUserIdentity(_currentUser.UserId, specialist.IdentityId);

            await _identityService.DeleteUserAsync(specialist.IdentityId);
        }
    }
}
