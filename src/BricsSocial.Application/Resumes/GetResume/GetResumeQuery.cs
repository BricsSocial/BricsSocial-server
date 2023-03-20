using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Resumes.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.GetResume
{
    public record GetResumeQuery : IRequest<ResumeDto>
    {
        public int? Id { get; init; }
    }

    public sealed class GetResumeQueryHandler : IRequestHandler<GetResumeQuery, ResumeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetResumeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResumeDto> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes
               .AsNoTracking()
               .Where(a => a.Id == request.Id)
               .ProjectTo<ResumeDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            if (resume == null)
                throw new NotFoundException(nameof(resume), request.Id);

            return resume;
        }
    }
}
