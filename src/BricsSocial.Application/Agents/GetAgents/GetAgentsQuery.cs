using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.GetAgents
{
    public sealed class GetAgentsQuery : PageQuery, IRequest<PaginatedList<AgentDto>>
    {
        public int? CompanyId { get; init; }
    }

    public sealed class GetAgentsQueryhandler : IRequestHandler<GetAgentsQuery, PaginatedList<AgentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAgentsQueryhandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AgentDto>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
        {
            var agents = await _context.Agents
                .Where(a => request.CompanyId == null || a.CompanyId == request.CompanyId)
                .OrderBy(a => a.Id)
                .ProjectTo<AgentDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return agents;
        }
    }
}
