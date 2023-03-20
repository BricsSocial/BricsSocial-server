using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Companies.Common;
using BricsSocial.Application.Vacancies.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.GetCompanies
{
    public sealed class GetCompaniesQuery : PageQuery, IRequest<PaginatedList<CompanyDto>>
    {
        public int? CountryId { get; init; }
    }

    public sealed class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, PaginatedList<CompanyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompaniesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _context.Companies
               .AsNoTracking()
               .Where(v => request.CountryId == null || v.CountryId == request.CountryId)
               .OrderBy(v => v.Id)
               .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize);

            return companies;
        }
    }
}
