using AutoMapper;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.RegisterAgent
{
    [Authorize(Roles = UserRoles.Administrator)]
    public record CreateAgentCommand : IRequest<AgentDto>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
        public int? CompanyId { get; init; }
    }

    public sealed class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, AgentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAgentCommandHandler> _logger;

        public CreateAgentCommandHandler(IApplicationDbContext context, IIdentityService identityService, IMapper mapper, ILogger<CreateAgentCommandHandler> logger)
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AgentDto> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            // TODO: This should be done under single transaction!

            var userInfo = new UserInfo
            {
                Role = UserRoles.Agent,
                Email = request.Email
            };
            (var result, var userId) = await _identityService.CreateUserAsync(userInfo, request.Password);

            if (!result.Succeeded)
                throw new Exception($"User creation error. {string.Join(", ", result.Errors)}");

            var agent = new Agent
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityId = userId,
                CompanyId = request.CompanyId.Value,
            };
            
            try
            {
                _context.Agents.Add(agent);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(Agent)} creation error");

                try
                {
                    await _identityService.DeleteUserAsync(userId);
                }
                catch (Exception ex1)
                {
                    _logger.LogCritical(ex1, $"Cannot delete invalid User ({userId})");
                }

                throw;
            }

            return _mapper.Map<AgentDto>(agent);
        }
    }
}
