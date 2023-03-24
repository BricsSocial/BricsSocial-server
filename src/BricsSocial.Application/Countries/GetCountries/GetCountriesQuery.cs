using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Countries.Common;
using BricsSocial.Application.Vacancies.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Countries.GetCountries
{
    public record GetCountriesQuery : IRequest<List<CountryDto>>
    {
    }

    public sealed class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries
                .AsNoTracking()
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.Id)
                .ToListAsync();

            return countries;
        }
    }
}
