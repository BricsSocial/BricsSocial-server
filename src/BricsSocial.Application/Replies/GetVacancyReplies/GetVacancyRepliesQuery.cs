using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Application.Specialists.Services;
using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.GetReplies
{
    [Authorize(Roles = UserRoles.Agent)]
    public sealed class GetVacancyRepliesQuery : PageQuery, IRequest<PaginatedList<ReplyDto>>
    {
        public bool? ByAgent { get; init; }

        public int? VacancyId { get; init; }

        public ReplyStatus? Status { get; init; }
        public ReplyType? Type { get; init; }

    }

    public sealed class GetRepliesQueryHandler : IRequestHandler<GetVacancyRepliesQuery, PaginatedList<ReplyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly IAgentService _agentService;

        public GetRepliesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, IAgentService agentService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _agentService = agentService;
        }

        public async Task<PaginatedList<ReplyDto>> Handle(GetVacancyRepliesQuery request, CancellationToken cancellationToken)
        {
            var agent = await _agentService.GetAgentByUserId(_currentUser.UserId);

            var replies = await _context.Replies
                .Where(r => r.Vacancy.CompanyId == agent.CompanyId)
                .Where(r => request.ByAgent == null || !request.ByAgent.Value || r.AgentId == agent.Id)
                .Where(r => request.VacancyId == null || request.VacancyId == r.VacancyId)
                .Where(r => request.Status == null || request.Status == r.Status)
                .Where(r => request.Type == null || request.Type == r.Type)
                .OrderByDescending(r => r.Id)
                .ProjectTo<ReplyDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return replies;
        }
    }
}
