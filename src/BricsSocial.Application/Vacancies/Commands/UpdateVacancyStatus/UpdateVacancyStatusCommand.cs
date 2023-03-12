using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Vacancies.Exceptions;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BricsSocial.Application.Vacancies.Commands.UpdateVacancyStatus
{
    [Authorize(Roles = UserRoles.Agent)]
    public record UpdateVacancyStatusCommand : IRequest<int>
    {
        public int? VacancyId { get; set; }
        public VacancyStatus VacancyStatus { get; set; }
    }

    public sealed class UpdateVacancyStatusCommandHandler : IRequestHandler<UpdateVacancyStatusCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateVacancyStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(UpdateVacancyStatusCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var agent = await _context.Agents
                .Where(a => a.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            var vacancy = await _context.Vacancies
                .Where(v => v.Id == request.VacancyId)
                .FirstOrDefaultAsync();

            if (vacancy is null)
                throw new NotFoundException(nameof(vacancy), request.VacancyId!);

            if (agent.CompanyId != vacancy.CompanyId)
                throw new AgentBelongsToOtherCompanyException(agent.Id, agent.CompanyId, vacancy.CompanyId);

            vacancy.Status = request.VacancyStatus;

            _context.Vacancies.Update(vacancy);

            await _context.SaveChangesAsync(cancellationToken);

            return vacancy.Id;
        }
    }
}
