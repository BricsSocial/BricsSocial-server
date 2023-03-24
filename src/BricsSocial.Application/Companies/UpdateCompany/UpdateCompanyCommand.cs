using AutoMapper;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Companies.Common;
using BricsSocial.Application.Vacancies.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.UpdateCompany
{
    [Authorize(Roles = UserRoles.Agent)]
    public record UpdateCompanyCommand : IRequest<CompanyDto>
    {
        public int? Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Logo { get; init; }
    }

    public sealed class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IAgentService _agentService;

        public UpdateCompanyCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser, IAgentService agentService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
            _agentService = agentService;
        }

        public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Where(v => v.Id == request.Id)
                .FirstOrDefaultAsync();

            if (company is null)
                throw new NotFoundException(nameof(company), request.Id!);

            await _agentService.CheckAgentBelongsToCompany(_currentUser.UserId!, company.Id);

            if (request.Name != null)
                company.Name = request.Name;
            if (request.Description != null)
                company.Description = request.Description;
            if (request.Logo != null)
                company.Logo = request.Logo;

            _context.Companies.Update(company);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CompanyDto>(company);
        }
    }
}
