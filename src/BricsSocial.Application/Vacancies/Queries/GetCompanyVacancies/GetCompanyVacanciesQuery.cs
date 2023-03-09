using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Queries.GetCompanyVacancies
{
    [Authorize(Roles = UserRoles.Agent)]
    public record GetCompanyVacanciesQuery : IRequest<CompanyVacanciesVm>
    {
        public string? CompanyId { get; set; }
    }

    public sealed class GetCompanyVacanciesQueryHandler : IRequestHandler<GetCompanyVacanciesQuery, CompanyVacanciesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetCompanyVacanciesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<CompanyVacanciesVm> Handle(GetCompanyVacanciesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var agent = await _context.Agents
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            var company = await _context.Companies
                .Where(c => c.Id == request.CompanyId)
                .FirstOrDefaultAsync();

            if (company is null)
                throw new NotFoundException(nameof(company), request.CompanyId);

            var vacancies = _context.Vacancies
                .Where(v => v.CompanyId == company.Id)
                .ToList();

            var response = new CompanyVacanciesVm
            {
                Company = new CompanyDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    Description = company.Description
                },
                Vacancies = vacancies.Select(v => new VacancyDto
                {
                    Id = v.Id,
                    Name = v.Name,
                    Requirements = v.Requirements,
                    Offerings = v.Offerings,
                    Status = new VacancyStatusDto
                    {
                        Name = v.Status.ToString(),
                        Value = (int)v.Status
                    }
                }).ToList()
            };

            return response;
        }
    }
}
