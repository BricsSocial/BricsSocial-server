using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.SkillTags.Utils;
using BricsSocial.Application.Specialists.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.GetSpecialists
{
    public sealed class GetSpecialistsQuery : PageQuery, IRequest<PaginatedList<SpecialistDto>>
    {
        public int? CountryId { get; init; }
        public string? SkillTags { get; init; }
    }

    public sealed class GetSpecialistsQueryhandler : IRequestHandler<GetSpecialistsQuery, PaginatedList<SpecialistDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSpecialistsQueryhandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SpecialistDto>> Handle(GetSpecialistsQuery request, CancellationToken cancellationToken)
        {
            var requestedSkillTags = request.SkillTags == null ? new List<string>() : SkillTagsUtils.ParseTags(request.SkillTags);
            var specialists = await _context.Specialists
                .Where(s => request.CountryId == null || s.CountryId == request.CountryId)
                .MatchTags(request.SkillTags)
                .OrderByDescending(s => s.Id)
                .ProjectTo<SpecialistDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return specialists;
        }
    }
}
