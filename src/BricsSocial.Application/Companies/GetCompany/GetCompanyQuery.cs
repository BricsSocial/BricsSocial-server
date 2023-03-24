using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Companies.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.GetCompany
{
    public record class GetCompanyQuery : IRequest<CompanyDto>
    {
        public int? Id { get; init; }
    }

    public sealed class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
               .AsNoTracking()
               .Where(a => a.Id == request.Id)
               .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            if (company == null)
                throw new NotFoundException(nameof(company), request.Id);

            return company;
        }
    }
}
