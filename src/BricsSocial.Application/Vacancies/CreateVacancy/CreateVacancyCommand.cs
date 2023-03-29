using BricsSocial.Domain.Entities;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Vacancies.Common;
using AutoMapper;
using BricsSocial.Domain.Enums;

namespace BricsSocial.Application.Vacancies.CreateVacancy
{
    [Authorize(Roles = UserRoles.Agent)]
    public record CreateVacancyCommand : IRequest<VacancyDto>
    {
        public string? Name { get; init; }
        public string? Requirements { get; init; }
        public string? Offerings { get; init; }
        public string? SkillTags { get; init; }
    }

    public sealed class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, VacancyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateVacancyCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<VacancyDto> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var agent = await _context.Agents
                .Where(a => a.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            var vacancy = new Vacancy();
            vacancy.Name = request.Name;
            vacancy.Requirements = request.Requirements;
            vacancy.Offerings = request.Offerings;
            vacancy.CompanyId = agent.CompanyId;
            vacancy.SkillTags = request.SkillTags;
            vacancy.Status = VacancyStatus.Open;

            _context.Vacancies.Add(vacancy);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<VacancyDto>(vacancy);
        }
    }

}
