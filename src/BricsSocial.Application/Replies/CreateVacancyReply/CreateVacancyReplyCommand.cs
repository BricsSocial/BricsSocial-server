using AutoMapper;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Specialists.Services;
using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.CreateVacancyReply
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record CreateVacancyReplyCommand : IRequest<ReplyDto>
    {
        public int? VacancyId { get; init; }
    }

    public sealed class CreateVacancyReplyCommandHandler : IRequestHandler<CreateVacancyReplyCommand, ReplyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ISpecialistService _specialistService;

        public CreateVacancyReplyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, ISpecialistService specialistService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _specialistService = specialistService;
        }

        public async Task<ReplyDto> Handle(CreateVacancyReplyCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _specialistService.GetSpecialistByUserIdAsync(_currentUser.UserId);

            var vacancy = await _context.Vacancies
                .Where(v => v.Id == request.VacancyId)
                .FirstOrDefaultAsync();

            if (vacancy == null)
                throw new BadRequestException($"{nameof(vacancy)} with {nameof(request.VacancyId)} = {request.VacancyId} not found");

            if (vacancy.Status == VacancyStatus.Closed)
                throw new BadRequestException($"Cannot create reply to closed vacancy");

            var reply = new Reply();
            reply.Status = ReplyStatus.Pending;
            reply.Type = ReplyType.Vacancy;
            reply.SpecialistId = specialist.Id;
            reply.VacancyId = vacancy.Id;

            _context.Replies.Add(reply);

            await _context.SaveChangesAsync(cancellationToken);

            reply.Specialist = specialist;
            reply.Vacancy = vacancy;

            return _mapper.Map<ReplyDto>(reply);
        }
    }
}
