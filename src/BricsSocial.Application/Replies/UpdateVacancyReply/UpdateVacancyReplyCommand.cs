using AutoMapper;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Specialists.Services;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.UpdateVacancyReply
{
    [Authorize(Roles = UserRoles.Agent)]
    public record UpdateVacancyReplyCommand : IRequest<ReplyDto>
    {
        public int? Id { get; init; }

        public ReplyStatus? Status { get; init; } 
    }

    public sealed class UpdateVacancyReplyCommandHandler : IRequestHandler<UpdateVacancyReplyCommand, ReplyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly IAgentService _agentService;

        public UpdateVacancyReplyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, IAgentService agentService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _agentService = agentService;
        }

        public async Task<ReplyDto> Handle(UpdateVacancyReplyCommand request, CancellationToken cancellationToken)
        {
            var agent = await _agentService.GetAgentByUserIdAsync(_currentUser.UserId);

            var reply = await _context.Replies
                .Where(r => r.Id == request.Id)
                .FirstOrDefaultAsync();

            if (reply == null)
                throw new NotFoundException(nameof(reply), request.Id);

            if (reply.Type == ReplyType.Specialist)
                throw new Exception("Cannot update specialist reply");

            reply.Status = request.Status.Value;
            reply.AgentId = agent.Id;

            _context.Replies.Update(reply);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReplyDto>(reply);
        }
    }
}
