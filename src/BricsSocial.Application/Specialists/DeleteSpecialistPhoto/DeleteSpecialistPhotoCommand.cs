using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.DeleteSpecialistPhoto
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record class DeleteSpecialistPhotoCommand : IRequest
    {
        public int? Id { get; init; }
    }

    public sealed class DeleteSpecialistPhotoCommandHandler : IRequestHandler<DeleteSpecialistPhotoCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IFileStorage _fileStorage;

        public DeleteSpecialistPhotoCommandHandler(IApplicationDbContext context, IUserService userService, ICurrentUserService currentUser, IFileStorage fileStorage)
        {
            _context = context;
            _userService = userService;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
        }

        public async Task Handle(DeleteSpecialistPhotoCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _context.Specialists
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (specialist == null)
                throw new NotFoundException(nameof(specialist), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, specialist.IdentityId);

            if (specialist.Photo == null)
                return;
            
            await _fileStorage.DeleteFileAsync(specialist.Photo, cancellationToken);

            specialist.Photo = null;
            _context.Specialists.Update(specialist);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
