using AutoMapper;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.RegisterAgent
{
    public record CreateAgentCommand : IRequest<AgentDto>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Position { get; set; }
        public int? CompanyId { get; set; }
    }

    public sealed class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, AgentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public CreateAgentCommandHandler(IApplicationDbContext context, IIdentityService identityService, IMapper mapper)
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<AgentDto> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            // TODO: This should be done under single transaction!

            var userInfo = new UserInfo
            {
                Role = UserRoles.Agent,
                Email = request.Email
            };
            (var result, var userId) = await _identityService.CreateUserAsync(userInfo, request.Password);

            if (!result.Succeeded)
                throw new Exception($"User creation error. {string.Join(", ", result.Errors)}");

            var agent = new Agent
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityId = userId,
                Position = request.Position,
                CompanyId = request.CompanyId.Value,
            };
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AgentDto>(agent);
        }
    }
}
