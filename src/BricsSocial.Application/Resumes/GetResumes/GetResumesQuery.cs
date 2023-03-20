using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Resumes.Common;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.GetResumes
{
    public sealed class GetResumesQuery : PageQuery, IRequest<PaginatedList<ResumeDto>>
    {
        public int? CountryId { get; init; }

        public List<int>? SkillTagsIds { get; init; }
    }

    public sealed class GetResumesQueryHandler : IRequestHandler<GetResumesQuery, PaginatedList<ResumeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetResumesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ResumeDto>> Handle(GetResumesQuery request, CancellationToken cancellationToken)
        {
            var skillTagsSet = request.SkillTagsIds?.ToHashSet() ?? new HashSet<int>();
            var resumes = await _context.Resumes
                .AsNoTracking()
                .Where(r => request.CountryId == null || r.Specialist.CountryId == request.CountryId)
                .Where(r => request.SkillTagsIds == null || r.SkillTags.Any(s => skillTagsSet.Contains(s.Id)))
                .OrderBy(r => r.Id)
                .ProjectTo<ResumeDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return resumes;
        }
    }
}
