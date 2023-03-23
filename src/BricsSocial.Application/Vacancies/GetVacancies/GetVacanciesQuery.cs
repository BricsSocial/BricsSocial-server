using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.SkillTags.Utils;
using BricsSocial.Application.Vacancies.Common;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.GetVacancies
{
    public sealed class GetVacanciesQuery : PageQuery, IRequest<PaginatedList<VacancyDto>>
    {
        public int? CountryId { get; init; }
        public int? CompanyId { get; init; }
        public VacancyStatus? Status { get; init; }

        public string? SkillTags { get; init; }
    }

    public sealed class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, PaginatedList<VacancyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetVacanciesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<VacancyDto>> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var requestedSkillTags = request.SkillTags == null ? new List<string>() : SkillTagsUtils.ParseTags(request.SkillTags);
            var vacancies = await _context.Vacancies
                .AsNoTracking()
                .Where(v => request.CompanyId == null || v.CompanyId == request.CompanyId)
                .Where(v => request.CountryId == null || v.Company.CountryId == request.CountryId)
                .Where(v => request.Status == null || v.Status == request.Status)
                .MatchTags(request.SkillTags)
                .OrderByDescending(v => v.Id)
                .ProjectTo<VacancyDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return vacancies;
        }
    }
}
