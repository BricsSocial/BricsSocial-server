using BricsSocial.Application.Agents.Services;
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

namespace BricsSocial.Application.Agents.DeleteAgent
{
    [Authorize(Roles = UserRoles.AdministratorAndAgent)]
    public record class DeleteAgentCommand : IRequest
    {
        public int? Id { get; init; }
    }

    public sealed class DeleteAgentCommandHandler : IRequestHandler<DeleteAgentCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;

        public DeleteAgentCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUser, IUserService userService)
        {
            _context = context;
            _identityService = identityService;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents
               .Where(c => c.Id == request.Id)
               .FirstOrDefaultAsync();

            if (agent == null)
                throw new NotFoundException(nameof(agent), request.Id);

            if (_currentUser.Role == UserRoles.Agent)
                _userService.CheckUserIdentity(_currentUser.UserId, agent.IdentityId);

            await _identityService.DeleteUserAsync(agent.IdentityId);
        }
    }
}
