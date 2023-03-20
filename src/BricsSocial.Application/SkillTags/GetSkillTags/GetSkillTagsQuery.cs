using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Countries.Common;
using BricsSocial.Application.SkillTags.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.SkillTags.GetSkillTags
{
    public record GetSkillTagsQuery : IRequest<List<SkillTagDto>>
    {
    }

    public sealed class GetSkillTagsQueryHandler : IRequestHandler<GetSkillTagsQuery, List<SkillTagDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSkillTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SkillTagDto>> Handle(GetSkillTagsQuery request, CancellationToken cancellationToken)
        {
            var skillTags = await _context.SkillTags
                .AsNoTracking()
                .ProjectTo<SkillTagDto>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToListAsync();

            return skillTags;
        }
    }
}
