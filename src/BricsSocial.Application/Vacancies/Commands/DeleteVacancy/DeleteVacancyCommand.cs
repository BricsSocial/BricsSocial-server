using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Vacancies.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BricsSocial.Application.Vacancies.Commands.DeleteVacancy
{
    [Authorize(Roles = UserRoles.Agent)]
    public record DeleteVacancyCommand : IRequest<string>
    {
        public string? VacancyId { get; set; }
    }

    public sealed class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeleteVacancyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<string> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var agent = await _context.Agents
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            var vacancy = await _context.Vacancies
                .Where(v => v.Id == request.VacancyId)
                .FirstOrDefaultAsync();

            if (vacancy is null)
                throw new NotFoundException(nameof(vacancy), request.VacancyId);

            if (agent.CompanyId != vacancy.CompanyId)
                throw new AgentBelongsToOtherCompanyException(agent.Id, agent.CompanyId, vacancy.CompanyId);

            _context.Vacancies.Remove(vacancy);

            await _context.SaveChangesAsync(cancellationToken);

            return vacancy.Id;
        }
    }
}
