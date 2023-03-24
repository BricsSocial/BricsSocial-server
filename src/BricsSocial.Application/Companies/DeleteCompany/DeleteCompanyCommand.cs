using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.DeleteCompany
{
    [Authorize(Roles = UserRoles.Administrator)]
    public record class DeleteCompanyCommand : IRequest
    {
        public int? Id { get; init; }
    }

    public sealed class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCompanyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();

            if (company == null)
                throw new NotFoundException(nameof(company), request.Id);

            _context.Companies.Remove(company);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
