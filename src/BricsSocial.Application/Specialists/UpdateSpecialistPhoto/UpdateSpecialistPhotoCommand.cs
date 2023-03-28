using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Specialists.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.UpdateSpecialistPhoto
{
    [Authorize(Roles = UserRoles.Specialist)]
    public sealed record UpdateSpecialistPhotoCommand : IRequest<FileUploadResponse>
    {
        public int? Id { get; set; }
        public IFormFile? File { get; init; }
    }

    public sealed class UpdateSpecialistPhotoCommandHandler : IRequestHandler<UpdateSpecialistPhotoCommand, FileUploadResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IFileStorage _fileStorage;
        private readonly ILogger _logger;

        public UpdateSpecialistPhotoCommandHandler(IApplicationDbContext context, IUserService userService, ICurrentUserService currentUser, IFileStorage fileStorage, ILogger<UpdateSpecialistPhotoCommandHandler> logger)
        {
            _context = context;
            _userService = userService;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
            _logger = logger;
        }

        public async Task<FileUploadResponse> Handle(UpdateSpecialistPhotoCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _context.Specialists
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (specialist == null)
                throw new NotFoundException(nameof(specialist), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, specialist.IdentityId);

            var fileUploadInfo = new FileUploadInfo
            {
                FileName = request.File.FileName,
                FolderName = FileConstants.SpecialistsFolder,
                InputStream = request.File.OpenReadStream()
            };

            try
            {
                if (specialist.Photo != null)
                    await _fileStorage.DeleteFileAsync(specialist.Photo, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete existing photo ({specialist.Photo})");
            }

            var fileUpdateResponse = await _fileStorage.UploadFileAsync(fileUploadInfo, cancellationToken);

            specialist.Photo = fileUpdateResponse.FileUrl;

            _context.Specialists.Update(specialist);

            await _context.SaveChangesAsync(cancellationToken);

            return fileUpdateResponse;
        }
    }
}
