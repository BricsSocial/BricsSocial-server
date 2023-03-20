using AutoMapper;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Resumes.Common;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.UpdateResume
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record UpdateResumeCommand : IRequest<ResumeDto>
    {
        public int? Id { get; init; }
        public string? Skills { get; init; }
        public string? Experience { get; init; }
        public List<int>? SkillTagsIds { get; init; }
    }

    public sealed class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, ResumeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateResumeCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, IUserService userService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ResumeDto> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes
                .Where(r => r.Id == request.Id)
                .Include(r => r.Specialist)
                .Include(r => r.SkillTags)
                .FirstOrDefaultAsync();

            if (resume is null)
                throw new NotFoundException(nameof(resume), request.Id!);

            _userService.CheckUserIdentity(_currentUser.UserId, resume.Specialist.IdentityId);

            if (request.Skills != null)
                resume.Skills = request.Skills;
            if (request.Experience != null)
                resume.Experience = request.Experience;

            if (request.SkillTagsIds != null)
            {
                var skillTags = _context.SkillTags.Where(s => request.SkillTagsIds.Contains(s.Id));

                resume.SkillTags = await skillTags.ToListAsync();
            }

            _context.Resumes.Update(resume);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ResumeDto>(resume);
        }
    }
}
