using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.UpdateAgentPhotoCommand
{
    [Authorize(Roles = UserRoles.Agent)]
    public sealed record UpdateAgentPhotoCommand : IRequest<FileUploadResponse>
    {
        public int? Id { get; set; }
        public IFormFile? File { get; init; }
    }

    public sealed class UpdateAgentPhotoCommandHandler : IRequestHandler<UpdateAgentPhotoCommand, FileUploadResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IFileStorage _fileStorage;
        private readonly ILogger _logger;

        public UpdateAgentPhotoCommandHandler(IApplicationDbContext context, IUserService userService, ICurrentUserService currentUser, IFileStorage fileStorage, ILogger<UpdateAgentPhotoCommandHandler> logger)
        {
            _context = context;
            _userService = userService;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
            _logger = logger;
        }

        public async Task<FileUploadResponse> Handle(UpdateAgentPhotoCommand request, CancellationToken cancellationToken)
        {
            var agent = await _context.Agents
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (agent == null)
                throw new NotFoundException(nameof(agent), request.Id);

            _userService.CheckUserIdentity(_currentUser.UserId, agent.IdentityId);

            var fileUploadInfo = new FileUploadInfo
            {
                FileName = request.File.FileName,
                FolderName = FileConstants.AgentsFolder,
                InputStream = request.File.OpenReadStream()
            };

            try
            {
                if (agent.Photo != null)
                    await _fileStorage.DeleteFileAsync(agent.Photo, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete existing photo ({agent.Photo})");
            }

            var fileUpdateResponse = await _fileStorage.UploadFileAsync(fileUploadInfo, cancellationToken);

            agent.Photo = fileUpdateResponse.FileUrl;

            _context.Agents.Update(agent);

            await _context.SaveChangesAsync(cancellationToken);

            return fileUpdateResponse;
        }
    }
}
