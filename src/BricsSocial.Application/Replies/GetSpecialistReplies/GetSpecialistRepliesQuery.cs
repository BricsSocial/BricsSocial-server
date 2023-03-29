using AutoMapper;
using AutoMapper.QueryableExtensions;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Specialists.Services;
using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.GetSpecialistReplies
{
    [Authorize(Roles = UserRoles.Specialist)]
    public sealed class GetSpecialistRepliesQuery : PageQuery, IRequest<PaginatedList<ReplyDto>>
    {
        public int? CompanyId { get; init; }
        public int? VacancyId { get; init; }

        public ReplyStatus? Status { get; init; }
        public ReplyType? Type { get; init; }
    }

    public sealed class GetSpecialistRepliesQueryHandler : IRequestHandler<GetSpecialistRepliesQuery, PaginatedList<ReplyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ISpecialistService _specialistService;

        public GetSpecialistRepliesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, ISpecialistService specialistService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _specialistService = specialistService;
        }

        public async Task<PaginatedList<ReplyDto>> Handle(GetSpecialistRepliesQuery request, CancellationToken cancellationToken)
        {
            var specialist = await _specialistService.GetSpecialistByUserIdAsync(_currentUser.UserId);

            var replies = await _context.Replies
                .Where(r => r.SpecialistId == specialist.Id)
                .Where(r => request.CompanyId == null || request.CompanyId == r.Vacancy.CompanyId)
                .Where(r => request.VacancyId == null || request.VacancyId == r.VacancyId)
                .Where(r => request.Status == null || request.Status == r.Status)
                .Where(r => request.Type == null || request.Type == r.Type)
                .OrderByDescending(r => r.Id)
                .ProjectTo<ReplyDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return replies;
        }
    }
}
