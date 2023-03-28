using AutoMapper;
using BricsSocial.Application.Agents.Services;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.UpdateCompanyLogo
{

    [Authorize(Roles = UserRoles.Agent)]
    public record UpdateCompanyLogoCommand : IRequest<FileUploadResponse>
    {
        public int? Id { get; set; }
        public IFormFile? File { get; init; }
    }

    public sealed class UpdateCompanyLogoCommandHandler : IRequestHandler<UpdateCompanyLogoCommand, FileUploadResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IAgentService _agentService;
        private readonly IFileStorage _fileStorage;
        private readonly ILogger _logger;

        public UpdateCompanyLogoCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IAgentService agentService, IFileStorage fileStorage, ILogger<UpdateCompanyLogoCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _agentService = agentService;
            _fileStorage = fileStorage;
            _logger = logger;
        }

        public async Task<FileUploadResponse> Handle(UpdateCompanyLogoCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Where(v => v.Id == request.Id)
                .FirstOrDefaultAsync();

            if (company is null)
                throw new NotFoundException(nameof(company), request.Id!);

            await _agentService.CheckAgentBelongsToCompanyAsync(_currentUser.UserId!, company.Id);

            var fileUploadInfo = new FileUploadInfo
            {
                FileName = request.File.FileName,
                FolderName = FileConstants.CompaniesFolder,
                InputStream = request.File.OpenReadStream()
            };

            try
            {
                if (company.Logo != null)
                    await _fileStorage.DeleteFileAsync(company.Logo, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete existing logo ({company.Logo})");
            }

            var fileUpdateResponse = await _fileStorage.UploadFileAsync(fileUploadInfo, cancellationToken);

            company.Logo = fileUpdateResponse.FileUrl;

            _context.Companies.Update(company);

            await _context.SaveChangesAsync(cancellationToken);

            return fileUpdateResponse;
        }
    }
}
