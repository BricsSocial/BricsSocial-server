using AutoMapper;
using BricsSocial.Application.Agents.Services;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Specialists.Services;
using BricsSocial.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.UpdateSpecialistReply
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record UpdateSpecialistReplyCommand : IRequest<ReplyDto>
    {
        public int? Id { get; init; }

        public ReplyStatus? Status { get; init; }
    }

    public sealed class UpdateSpecialistReplyCommandHandler : IRequestHandler<UpdateSpecialistReplyCommand, ReplyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ISpecialistService _specialistService;

        public UpdateSpecialistReplyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper, ISpecialistService specialistService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _specialistService = specialistService;
        }

        public async Task<ReplyDto> Handle(UpdateSpecialistReplyCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _specialistService.GetSpecialistByUserIdAsync(_currentUser.UserId);

            var reply = await _context.Replies
                .Where(r => r.Id == request.Id)
                .Include(r => r.Agent)
                .Include(r => r.Specialist)
                .Include(r => r.Vacancy)
                .FirstOrDefaultAsync();

            if (reply == null)
                throw new NotFoundException(nameof(reply), request.Id);

            if (reply.Type == ReplyType.Vacancy)
                throw new Exception("Cannot update vacancy reply");

            reply.Status = request.Status.Value;

            _context.Replies.Update(reply);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReplyDto>(reply);
        }
    }
}
