using AutoMapper;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Specialists.Services;
using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.CreateSpecialistReply
{
    [Authorize(Roles = UserRoles.Agent)]
    public record CreateSpecialistReplyCommand : IRequest<ReplyDto>
    {
        public int? SpecialistId { get; init; }
        public int? VacancyId { get; init; }
    }

    public sealed class CreateSpecialistReplyCommandHandler : IRequestHandler<CreateSpecialistReplyCommand, ReplyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly IAgentService _agentService;

        public CreateSpecialistReplyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, IAgentService agentService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _agentService = agentService;
        }

        public async Task<ReplyDto> Handle(CreateSpecialistReplyCommand request, CancellationToken cancellationToken)
        {
            var agent = await _agentService.GetAgentByUserIdAsync(_currentUser.UserId);

            var specialist = await _context.Specialists
                .Where(v => v.Id == request.SpecialistId)
                .FirstOrDefaultAsync();

            if (specialist == null)
                throw new BadRequestException($"{nameof(specialist)} with {nameof(request.SpecialistId)} = {request.SpecialistId} not found");

            var vacancy = await _context.Vacancies
                .Where(v => v.Id == request.VacancyId)
                .FirstOrDefaultAsync();

            if (vacancy == null)
                throw new BadRequestException($"{nameof(vacancy)} with {nameof(request.VacancyId)} = {request.VacancyId} not found");
            if (vacancy.Status == VacancyStatus.Closed)
                throw new BadRequestException($"Cannot create reply with closed vacancy");

            var reply = new Reply();
            reply.Status = ReplyStatus.Pending;
            reply.Type = ReplyType.Specialist;
            reply.AgentId = agent.Id;
            reply.SpecialistId = specialist.Id;
            reply.VacancyId = vacancy.Id;

            _context.Replies.Add(reply);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReplyDto>(reply);
        }
    }
}
