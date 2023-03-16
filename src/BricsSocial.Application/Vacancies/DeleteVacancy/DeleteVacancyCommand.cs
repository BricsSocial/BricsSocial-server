using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Vacancies.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BricsSocial.Application.Vacancies.DeleteVacancy
{
    [Authorize(Roles = UserRoles.Agent)]
    public record DeleteVacancyCommand : IRequest
    {
        public int? Id { get; set; }
    }

    public sealed class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeleteVacancyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var agent = await _context.Agents
                .Where(a => a.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            var vacancy = await _context.Vacancies
                .Where(v => v.Id == request.Id)
                .FirstOrDefaultAsync();

            if (vacancy is null)
                throw new NotFoundException(nameof(vacancy), request.Id);

            if (agent.CompanyId != vacancy.CompanyId)
                throw new AgentBelongsToOtherCompanyException(agent.Id, agent.CompanyId, vacancy.CompanyId);

            _context.Vacancies.Remove(vacancy);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
