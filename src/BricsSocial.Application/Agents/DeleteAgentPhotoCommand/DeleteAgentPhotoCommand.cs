using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.DeleteAgentPhotoCommand
{
    [Authorize(Roles = UserRoles.Agent)]
    public record class DeleteAgentPhotoCommand : IRequest
    {
        public int? Id { get; init; }
    }

    public sealed class DeleteAgentPhotoCommandHandler : IRequestHandler<DeleteAgentPhotoCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IFileStorage _fileStorage;

        public DeleteAgentPhotoCommandHandler(IApplicationDbContext context, IUserService userService, ICurrentUserService currentUser, IFileStorage fileStorage)
        {
            _context = context;
            _userService = userService;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
        }

        public async Task Handle(DeleteAgentPhotoCommand request, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (agent == null)
                throw new NotFoundException(nameof(agent), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, agent.IdentityId);

            if (agent.Photo == null)
                return;

            await _fileStorage.DeleteFileAsync(agent.Photo, cancellationToken);

            agent.Photo = null;
            _context.Agents.Update(agent);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
