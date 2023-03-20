using AutoMapper;
using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Vacancies.Common;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BricsSocial.Application.Vacancies.UpdateVacancy
{
    [Authorize(Roles = UserRoles.Agent)]
    public record UpdateVacancyCommand : IRequest<VacancyDto>
    {
        public int? Id { get; init; }
        public string? Name { get; init; }
        public string? Requirements { get; init; }
        public string? Offerings { get; init; }
        public VacancyStatus? Status { get; init; }
    }

    public sealed class UpdateVacancyCommandHandler : IRequestHandler<UpdateVacancyCommand, VacancyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public UpdateVacancyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<VacancyDto> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!;
            var agent = await _context.Agents
                .Where(a => a.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            var vacancy = await _context.Vacancies
                .Where(v => v.Id == request.Id)
                .FirstOrDefaultAsync();

            if (vacancy is null)
                throw new NotFoundException(nameof(vacancy), request.Id!);

            if (agent.CompanyId != vacancy.CompanyId)
                throw new AgentBelongsToOtherCompanyException(agent.Id, agent.CompanyId, vacancy.CompanyId);

            if (request.Name != null)
                vacancy.Name = request.Name;
            if (request.Requirements != null)
                vacancy.Requirements = request.Requirements;
            if (request.Offerings != null)
                vacancy.Offerings = request.Offerings;
            if(request.Status != null)
                vacancy.Status = request.Status.Value;

            _context.Vacancies.Update(vacancy);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<VacancyDto>(vacancy);
        }
    }
}
