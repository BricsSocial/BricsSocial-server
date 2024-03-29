﻿using AutoMapper;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.UpdateSpecialist
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record UpdateSpecialistCommand : IRequest<SpecialistDto>
    {
        public int? Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        public string? Bio { get; init; }
        public string? About { get; init; }
        public string? SkillTags { get; init; }
        public string? Photo { get; init; }
    }

    public sealed class UpdateSpecialistCommandHandler : IRequestHandler<UpdateSpecialistCommand, SpecialistDto>
    {
        public readonly IApplicationDbContext _context;
        public readonly IMapper _mapper;
        public readonly ICurrentUserService _currentUser;
        public readonly IUserService _userService;

        public UpdateSpecialistCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<SpecialistDto> Handle(UpdateSpecialistCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _context.Specialists
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (specialist == null)
                throw new NotFoundException(nameof(specialist), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, specialist.IdentityId);

            if (request.FirstName != null)
                specialist.FirstName = request.FirstName;
            if (request.LastName != null)
                specialist.LastName = request.LastName;
            if (request.Bio != null)
                specialist.Bio = request.Bio;
            if (request.About != null)
                specialist.About = request.About;
            if (request.SkillTags != null)
                specialist.SkillTags = request.SkillTags;
            if (request.Photo != null)
                specialist.Photo = request.Photo;

            _context.Specialists.Update(specialist);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SpecialistDto>(specialist);
        }
    }
}
