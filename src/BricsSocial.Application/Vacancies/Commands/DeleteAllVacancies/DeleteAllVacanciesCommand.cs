using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;

namespace BricsSocial.Application.Vacancies.Commands.DeleteAllVacancies
{
    [Authorize(Roles = UserRoles.Administrator)]
    public record DeleteAllVacanciesCommand : IRequest;

    public sealed class DeleteAllVacanciesHandler : IRequestHandler<DeleteAllVacanciesCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAllVacanciesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAllVacanciesCommand request, CancellationToken cancellationToken)
        {
            // bad approach
            _context.Vacancies.RemoveRange(_context.Vacancies);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
