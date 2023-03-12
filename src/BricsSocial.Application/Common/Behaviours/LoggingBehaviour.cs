using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace BricsSocial.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        UserInfo? userInfo = null;

        if (!string.IsNullOrEmpty(userId))
        {
            userInfo = await _identityService.GetUserInfoAsync(userId);
        }

        _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userInfo, request);
    }
}
