using BricsSocial.Domain.Entities;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BricsSocial.Application.Common.Exceptions;

namespace BricsSocial.Application.Vacancies.Commands.CreateVacancy
{
    [Authorize(Roles = UserRoles.Agent)]
    public record CreateVacancyCommand : IRequest<int>
    {
        public string? Name { get; set; }
        public string? Requirements { get; set; }
        public string? Offerings { get; set; }
    }

    public sealed class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateVacancyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
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

            _context.Vacancies.Add(vacancy);

            await _context.SaveChangesAsync(cancellationToken);

            return vacancy.Id;
        }
    }

}
