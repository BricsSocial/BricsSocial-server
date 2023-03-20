using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Specialists.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.GetSpecialist
{
    public record GetSpecialistQuery : IRequest<SpecialistDto>
    {
        public int? Id { get; init; }
    }

    public record GetSpecialistQueryHandler : IRequestHandler<GetSpecialistQuery, SpecialistDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSpecialistQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SpecialistDto> Handle(GetSpecialistQuery request, CancellationToken cancellationToken)
        {
            var specialist = await _context.Specialists
                .Where(a => a.Id == request.Id)
                .ProjectTo<SpecialistDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (specialist == null)
                throw new NotFoundException(nameof(specialist), request.Id);

            return specialist;
        }
    }
}