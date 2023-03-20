using AutoMapper;
using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Resumes.Common;
using BricsSocial.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.CreateResume
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record CreateResumeCommand : IRequest<ResumeDto>
    {
        public string? Skills { get; init; }
        public string? Experience { get; init; }

        public List<int>? SkillTagsIds { get; init; }
    }

    public sealed class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, ResumeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateResumeCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ResumeDto> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var specialist = await _context.Specialists
                .Include(s => s.Resume)
                .Where(s => s.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (specialist is null)
                throw new SpecialistUserNotFound(userId);

            if (specialist.Resume != null)
                throw new ResumeAlreadyExists(specialist.Id, specialist.Resume.Id);

            var resume = new Resume();
            resume.Skills = request.Skills;
            resume.Experience = request.Experience;
            resume.SpecialistId = specialist.Id;

            if (request.SkillTagsIds != null)
            {
                var skillTags = _context.SkillTags.Where(s => request.SkillTagsIds.Contains(s.Id));

                resume.SkillTags.AddRange(skillTags);
            }

            _context.Resumes.Add(resume);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ResumeDto>(resume);
        }
    }
}
