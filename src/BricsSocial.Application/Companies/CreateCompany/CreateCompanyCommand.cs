using AutoMapper;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Companies.Common;
using BricsSocial.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.CreateCompany
{
    [Authorize(Roles = UserRoles.Administrator)]
    public record CreateCompanyCommand : IRequest<CompanyDto>
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Logo { get; init; }
        public int? CountryId { get; init; }
    }

    public sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company();
            company.Name = request.Name;
            company.Description = request.Description;
            company.Logo = request.Logo;
            company.CountryId = request.CountryId.Value;

            _context.Companies.Add(company);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CompanyDto>(company);
        }
    }
}
