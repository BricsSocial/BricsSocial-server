using AutoMapper;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.GetCurrentAgent
{
    [Authorize(Roles = UserRoles.Agent)]
    public record GetCurrentAgentQuery : IRequest<AgentDto>
    {

    }

    public record GetCurrentAgentQueryHandler : IRequestHandler<GetCurrentAgentQuery, AgentDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IAgentService _agentService;

        public GetCurrentAgentQueryHandler(IMapper mapper, ICurrentUserService currentUser, IAgentService agentService)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _agentService = agentService;
        }

        public async Task<AgentDto> Handle(GetCurrentAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = await _agentService.GetAgentByUserId(_currentUser.UserId);

            return _mapper.Map<AgentDto>(agent);
        }
    }
}
