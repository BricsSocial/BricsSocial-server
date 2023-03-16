using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.GetAgent
{
    public record GetAgentQuery : IRequest<AgentDto>
    {
        public int Id { get; set; }
    }

    public record GetAgentQueryHandler : IRequestHandler<GetAgentQuery, AgentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAgentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgentDto> Handle(GetAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents
                .Where(a => a.Id == request.Id)
                .ProjectTo<AgentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (agent == null)
                throw new NotFoundException(nameof(agent), request.Id);

            return agent;
        }
    }
}
