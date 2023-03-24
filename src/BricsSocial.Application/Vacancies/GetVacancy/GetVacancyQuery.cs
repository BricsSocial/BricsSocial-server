using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Exceptions;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Vacancies.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.GetVacancy
{
    public record GetVacancyQuery : IRequest<VacancyDto>
    {
        public int? Id { get; init; }
    }

    public sealed class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, VacancyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetVacancyQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacancyDto> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _context.Vacancies
               .AsNoTracking()
               .Where(a => a.Id == request.Id)
               .ProjectTo<VacancyDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            if (vacancy == null)
                throw new NotFoundException(nameof(vacancy), request.Id);

            return vacancy;
        }
    }
}
