using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.DeleteResume
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record DeleteResumeCommand : IRequest
    {
        public int? Id { get; init; }
    }

    public sealed class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;

        public DeleteResumeCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IUserService userService)
        {
            _context = context;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes
                .Include(r => r.Specialist)
                .Where(v => v.Id == request.Id)
                .FirstOrDefaultAsync();

            if (resume is null)
                throw new NotFoundException(nameof(resume), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, resume.Specialist.IdentityId);

            _context.Resumes.Remove(resume);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
