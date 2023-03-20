using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Common;
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

namespace BricsSocial.Application.Agents.UpdateAgent
{
    [Authorize(Roles = UserRoles.Agent)]
    public record UpdateAgentCommand : IRequest<AgentDto>
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }
    }

    public sealed class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, AgentDto>
    {
        public readonly IApplicationDbContext _context;
        public readonly IMapper _mapper;
        public readonly ICurrentUserService _currentUser;
        public readonly IUserService _userService;

        public UpdateAgentCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<AgentDto> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (agent == null)
                throw new NotFoundException(nameof(agent), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, agent.IdentityId);

            if (request.FirstName != null)
                agent.FirstName = request.FirstName;
            if (request.LastName != null)
                agent.LastName = request.LastName;
            if (request.Position != null)
                agent.Position = request.Position;
            if (request.Photo != null)
                agent.Photo = request.Photo;

            _context.Agents.Update(agent);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AgentDto>(agent);
        }
    }
}
